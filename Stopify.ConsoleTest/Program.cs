using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Infrastructure;

namespace Stopify.ConsoleTest;

// [ ] Replace nameof(class) in typeof(object) in entities, services etc.
// [ ] Replace entity's integer IDs to strongly typed IDs. (think about the conversion from integer to strongly typed ID)
// [ ] Rename Contracts to Abstractions.
// [ ] Put custom exceptions into Domain layer and structure them in folders. (Entities => {Entity} => {Entity}NotFoundException, {Entity}AlreadyExistsException)
// [ ] Replace services with CQS folders and classes (e.g. Create..., Get... etc) BUT in Application layer.
// [ ] Implement Create method to all entities.
// [ ] Put EFCore model configurations in Configurations folder (located in Persistence).
// [ ] Remake the Entities and DTOs so that the DTOs have the Annotations and fields only for Database needs.

internal class Program
{
    static async Task Main(string[] args)
    {
        ServiceProvider provider;
        IConfiguration configuration;

        var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Stopify.Presentation")))
                    .AddJsonFile("appsettings.json", false, true);
        configuration = builder.Build();

        var services = new ServiceCollection();
        services.AddInfrastructureDependencies(configuration);
        services.AddDomainDependencies();

        provider = services.BuildServiceProvider();


        var countryService = provider.GetRequiredService<ICountryService>();
        //await countryService.CreateAsync(new("Hungary"));
        //await countryService.AddArtistAsync("Hungary", "Young Fly");

        var genreService = provider.GetRequiredService<IGenreService>();
        //await genreService.CreateAsync(new("pop"));
        //await genreService.AddSongAsync("pop", "casa de papel");

        var songService = provider.GetRequiredService<ISongService>();
        //await songService.CreateAsync(new("3korty", 192, new DateOnly(2023, 5, 1), "azahriah_3korty"));
        //await songService.RemoveAsync(new("depicentrum (outro)"));
        //await songService.AddArtistAsync("3korty", "Azahriah");
        //await songService.UpdateTitleAsync("depicentrum (outro)", "depicentrum - outro");
        //await songService.UpdateAlbumAsync("introvertált dal", "memento");
        //await songService.UpdateAlbumAsync("delirium", "memento", 2);
        //await songService.UpdatePathAsync("vicc", "azahriah_desh_vicc");

        var artistService = provider.GetRequiredService<IArtistService>();
        //await artistService.CreateAsync(new("Young Fly", true, avatar: "young-fly"));
        //await artistService.UpdateCountryAsync("DESH", "Hungary");
        //await artistService.UpdateAvatarAsync("Young Fly", "young-fly");
        //await artistService.UpdateBgImageAsync("Young Fly", "young-fly");
        //await artistService.RemoveAsync(new("Queen Omega"));
        //await artistService.AddSongAsync("Azahriah", "introvertált dal");
        //await artistService.AddAlbumAsync("Azahriah", "memento");

        var albumService = provider.GetRequiredService<IAlbumService>();
        //await albumService.CreateAsync(new("memento", new(2023, 5, 1), "azahriah_memento"));
        //await albumService.RemoveAsync(new("memento"));
        //await albumService.AddSongAsync("memento", "depicentrum - outro");

        var playlistService = provider.GetRequiredService<IPlaylistService>();
        //await playlistService.CreateAsync(new("playlist2", true));
        //await playlistService.UpdateTitleAsync("myPlaylist", "playlist1");
        //await playlistService.AddSongAsync("playlist1", "four moods 2");
        //await playlistService.UpdateSongPositionAsync("playlist1", "four moods 2", 2);

        var userService = provider.GetRequiredService<IUserService>();
        //await userService.CreateAsync(User.Create("denestrambola", "Denes", "Trambola", "tramboladenes@gmail.com", "password"));

        //await PrintSongsByAlbum(songService, 1);
    }


    private static async Task PrintSongsByAlbum(ISongService service, int albumId)
    {
        IEnumerable<Song>? songs = await service.GetAllByAlbumIdAsync(albumId);
        if (songs.Count() == 0 || songs is null)
        {
            Console.WriteLine("There are no songs!");
            return;
        }

        Console.WriteLine("Songs:");
        foreach (var song in songs)
        {
            Console.WriteLine($"Title: {song.Title}");
            Console.WriteLine($"Album: {song.Album?.Title ?? "NOT-FOUND!"}");
            Console.WriteLine($"Duration: {song.Duration}");
            Console.WriteLine($"Release date: {song.ReleaseDate}\n");
        }
    }

    private static async Task PrintArtists(IArtistService service)
    {
        var artists = await service.GetAllAsync();
        if (artists.Count() == 0)
        {
            Console.WriteLine("There are no artists!");
            return;
        }

        Console.WriteLine("Artists:");
        foreach (var artist in artists)
        {
            Console.WriteLine($"Name: {artist.Name}");
            Console.WriteLine($"Bio: {artist.Bio}");
            Console.WriteLine($"Country: {artist.Country}");
            Console.WriteLine($"Avatar: {artist.Avatar}");
            Console.WriteLine($"BgImage: {artist.BgImage}");
            Console.WriteLine($"IsVerified: {artist.IsVerified}\n");
        }
    }

    private static async Task PrintAlbums(IAlbumService service)
    {
        var albums = await service.GetAllAsync();
        if (albums.Count() == 0)
        {
            Console.WriteLine("There are no albums!");
            return;
        }

        Console.WriteLine("Albums:");
        foreach (var album in albums)
        {
            Console.WriteLine($"Title: {album.Title}");
            Console.WriteLine($"ReleaseDate: {album.ReleaseDate}");
            Console.WriteLine($"Cover: {album.Cover}");
            Console.WriteLine($"Saves: {album.Saves}");
            Console.WriteLine($"Songs: {album.Songs}");
            Console.WriteLine($"Duration: {album.Duration}\n");
        }
    }

    private static async Task PrintSongs(ISongService service)
    {
        IEnumerable<Song>? songs = await service.GetAllAsync();
        if (songs.Count() == 0 || songs is null)
        {
            Console.WriteLine("There are no songs!");
            return;
        }

        Console.WriteLine("Songs:");
        foreach (var song in songs)
        {
            Console.WriteLine($"Title: {song.Title}");
            Console.WriteLine($"Album: {song.Album?.Title ?? "NOT-FOUND!"}");
            Console.WriteLine($"Duration: {song.Duration}");
            Console.WriteLine($"Release date: {song.ReleaseDate}\n");
        }
    }

    private static async Task PrintPlaylists(IPlaylistService service)
    {
        var playlists = await service.GetAllAsync();
        if (playlists.Count() == 0)
            Console.WriteLine("There are no playlists!");

        Console.WriteLine("Playlists:");
        foreach (var playlist in playlists)
        {
            Console.WriteLine("Id: " + playlist.Id);
            Console.WriteLine("Title: " + playlist.Title);
            Console.WriteLine("Description: " + playlist.Description);
            Console.WriteLine("IsPublic: " + playlist.IsPublic);
            Console.WriteLine("Saves: " + playlist.Saves);
            Console.WriteLine("Songs: " + playlist.Songs);
            Console.WriteLine("Duration: " + playlist.Duration);
            Console.WriteLine("Cover: " + playlist.Cover);
            Console.WriteLine();
        }
    }

    private static async Task PrintUsers(IUserService service)
    {
        var users = await service.GetAllAsync();
        if (users.Count() == 0)
            Console.WriteLine("There are no users!");

        Console.WriteLine("Users:");
        foreach (var user in users)
        {
            Console.WriteLine("Id: " + user.Id);
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("FirstName: " + user.FirstName);
            Console.WriteLine("LastName: " + user.LastName);
            Console.WriteLine("Email: " + user.Email);
            Console.WriteLine("PasswordHash: " + user.PasswordHash);
            Console.WriteLine("DateJoined: " + user.DateJoined);
            Console.WriteLine();
        }
    }
}
