using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Entities;

namespace Stopify.Infrastructure.Persistence;

public partial class StopifyDbContext : DbContext
{
    public StopifyDbContext() { }

    public StopifyDbContext(DbContextOptions<StopifyDbContext> options) : base(options) { }

    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<Artist> Artists { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Playlist> Playlists { get; set; }
    public virtual DbSet<SongQueue> Queues { get; set; }
    public virtual DbSet<RecentPlayed> RecentPlays { get; set; }
    public virtual DbSet<Song> Songs { get; set; }
    public virtual DbSet<SongPlaylist> SongPlaylists { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAlbum> UserAlbums { get; set; }
    public virtual DbSet<UserArtist> UserArtists { get; set; }
    public virtual DbSet<PlaybackHistory> PlaybackHistories { get; set; }
    public virtual DbSet<UserPlaylist> UserPlaylists { get; set; }
    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=tcp:stopify.database.windows.net,1433;Initial Catalog=Stopify;Persist Security Info=False;User ID=tramboladenes;Password=Asd12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Album>(entity =>
        {
            entity.Property(e => e.Cover)
                .HasMaxLength(2048)
                .IsUnicode(false);

            entity.Property(e => e.Duration);

            entity.Property(e => e.Title).HasMaxLength(50);
            entity.HasIndex(a => a.Title).IsUnique();

            entity.HasMany(d => d.Artists).WithMany(p => p.Albums)
                .UsingEntity<Dictionary<string, object>>(
                    "AlbumArtists",
                    r => r.HasOne<Artist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_AlbumArtists_Artists"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_AlbumArtists_Albums"),
                    j =>
                    {
                        j.HasKey("AlbumId", "ArtistId");
                        j.ToTable("AlbumArtists");
                    });

            entity.Property(e => e.Id).HasColumnName("AlbumId");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.Property(e => e.Avatar).HasMaxLength(2048);
            entity.Property(e => e.BgImage).HasMaxLength(2048);
            entity.Property(e => e.Bio).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.HasIndex(a => a.Name).IsUnique();

            entity.HasOne(d => d.Country).WithMany(p => p.Artists)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Artists_Countries");

            entity.Property(e => e.Id).HasColumnName("ArtistId");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Id).HasColumnName("CountryId");
            entity.HasIndex(a => a.Name).IsUnique();
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("GenreId");
            entity.HasIndex(a => a.Name).IsUnique();
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(200);

            entity.Property(e => e.Duration);

            entity.Property(e => e.Title).HasMaxLength(50);

            entity.Property(e => e.Id).HasColumnName("PlaylistId");

            entity.HasIndex(a => a.Title)
                .HasFilter("[IsPublic] = 1")
                .IsUnique();

            entity.Property(e => e.Cover)
                .HasMaxLength(2048)
                .HasColumnType("nvarchar");
        });

        modelBuilder.Entity<SongQueue>(entity =>
        {
            entity.ToTable("SongQueue");

            entity.HasOne(d => d.Song).WithMany(p => p.Queues)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SongQueue_Songs");

            entity.HasOne(d => d.User).WithMany(p => p.Queues)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SongQueue_Users");

            entity.Property(e => e.Id).HasColumnName("QueueId");
        });

        modelBuilder.Entity<RecentPlayed>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RecentPlays");

            entity.HasOne(d => d.Song).WithMany(p => p.RecentPlays)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RecentPlays_Songs");

            entity.HasOne(d => d.User).WithMany(p => p.RecentPlays)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RecentPlays_Users");

            entity.Property(e => e.Id).HasColumnName("RecentId");
        });

        modelBuilder.Entity<Song>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Song>>)(entity =>
        {
            entity.Property(e => e.Cover).HasMaxLength(2048).IsRequired();
            entity.Property(e => e.Duration);
            entity.Property(e => e.AlbumPosition);
            entity.Property(e => e.Path).HasMaxLength(2048);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.HasIndex(a => a.Title).IsUnique();

            RelationalForeignKeyBuilderExtensions.HasConstraintName<Album, Song>(entity.HasOne(d => d.Album).WithMany(p => p.SongsNavigation)
                .HasForeignKey((System.Linq.Expressions.Expression<Func<Song, object?>>)(d => d.AlbumId))
, "FK_Songs_Albums");

            entity.HasMany(d => d.Artists).WithMany(p => p.Songs)
                .UsingEntity<Dictionary<string, object>>(
                    "SongArtists",
                    r => (Microsoft.EntityFrameworkCore.Metadata.Builders.ReferenceCollectionBuilder<Artist, Dictionary<string, object>>)r.HasOne<Artist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SongArtists_Artists"),
                    l => (Microsoft.EntityFrameworkCore.Metadata.Builders.ReferenceCollectionBuilder<Song, Dictionary<string, object>>)l.HasOne<Song>().WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SongArtists_Songs"),
                    j =>
                    {
                        j.HasKey("SongId", "ArtistId");
                        j.ToTable("SongArtists");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Songs)
                .UsingEntity<Dictionary<string, object>>(
                    "SongGenre",
                    r => (Microsoft.EntityFrameworkCore.Metadata.Builders.ReferenceCollectionBuilder<Genre, Dictionary<string, object>>)r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SongGenres_Genres"),
                    l => (Microsoft.EntityFrameworkCore.Metadata.Builders.ReferenceCollectionBuilder<Song, Dictionary<string, object>>)l.HasOne<Song>().WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SongGenres_Songs"),
                    j =>
                    {
                        j.HasKey("SongId", "GenreId");
                        j.ToTable("SongGenres");
                    });

            entity.Property(e => e.Id).HasColumnName("SongId");
        }));

        modelBuilder.Entity<SongPlaylist>(entity =>
        {
            entity.HasKey(e => new { e.SongId, e.PlaylistId });

            entity.HasOne(d => d.Playlist).WithMany(p => p.SongPlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SongPlaylists_Playlists");

            entity.HasOne(d => d.Song).WithMany(p => p.SongPlaylists)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SongPlaylists_Songs");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.DateJoined)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Email).HasMaxLength(254);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(60);
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Avatar).HasMaxLength(2048).IsRequired();

            entity.HasIndex(a => a.Username).IsUnique();
            entity.HasIndex(a => a.Email).IsUnique();

            entity.Property(e => e.Id).HasColumnName("UserId");
        });

        modelBuilder.Entity<UserAlbum>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AlbumId });

            entity.Property(e => e.SavedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Album).WithMany(p => p.UserAlbums)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserAlbums_Albums");

            entity.HasOne(d => d.User).WithMany(p => p.UserAlbums)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserAlbums_Users");
        });

        modelBuilder.Entity<UserArtist>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ArtistId });

            entity.Property(e => e.FollowedDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Artist).WithMany(p => p.UserArtists)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserArtists_Artists");

            entity.HasOne(d => d.User).WithMany(p => p.UserArtists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserArtists_Users");
        });

        modelBuilder.Entity<PlaybackHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("PlaybackHistories");

            entity.Property(e => e.PlaybackDateTime)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");


            entity.HasOne(d => d.Song).WithMany(p => p.PlaybackHistories)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PlaybackHistories_Songs");

            entity.HasOne(d => d.User).WithMany(p => p.PlaybackHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PlaybackHistories_Users");

            entity.Property(e => e.Id).HasColumnName("HistoryId");
        });

        modelBuilder.Entity<UserPlaylist>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PlaylistId });

            entity.Property(e => e.SavedDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Playlist).WithMany(p => p.UserPlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserPlaylists_Playlists");

            entity.HasOne(d => d.User).WithMany(p => p.UserPlaylists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserPlaylists_Users");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId });

            entity.HasOne(e => e.User)
                  .WithMany(u => u.Followers)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Follower)
                  .WithMany(u => u.Followings)
                  .HasForeignKey(e => e.FollowerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.FollowedDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
