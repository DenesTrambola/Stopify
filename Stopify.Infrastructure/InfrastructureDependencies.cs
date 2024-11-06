using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Other;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Infrastructure.Other;
using Stopify.Infrastructure.Persistence;
using Stopify.Infrastructure.Persistence.Repositories;
using System.Net;
using System.Net.Mail;

namespace Stopify.Infrastructure;

public static class InfrastructureDependencies
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StopifyDbContext>(o => o.UseSqlServer(configuration.GetConnectionString(nameof(StopifyDbContext))))
            .AddSingleton<IAlbumRepository, AlbumRepository>()
            .AddSingleton<IArtistRepository, ArtistRepository>()
            .AddSingleton<ICountryRepository, CountryRepository>()
            .AddSingleton<IGenreRepository, GenreRepository>()
            .AddSingleton<IPlaybackHistoryRepository, PlaybackHistoryRepository>()
            .AddSingleton<IPlaylistRepository, PlaylistRepository>()
            .AddSingleton<IQueueRepository, QueueRepository>()
            .AddSingleton<IRecentPlayedRepository, RecentPlayedRepository>()
            .AddSingleton<ISongPlaylistRepository, SongPlaylistsRepository>()
            .AddSingleton<ISongRepository, SongRepository>()
            .AddSingleton<IUserAlbumRepository, UserAlbumRepository>()
            .AddSingleton<IUserArtistRepository, UserArtistRepository>()
            .AddSingleton<IUserPlaylistRepository, UserPlaylistRepository>()
            .AddSingleton<IUserFollowerRepository, UserFollowerRepository>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IUnitOfWork, UnitOfWork>()

            .AddTransient<IEmailSender, EmailSender>()
            .AddSingleton(new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tramboladenes@gmail.com", configuration.GetConnectionString(nameof(EmailSender))),
                EnableSsl = true
            })

            .AddSingleton<IAudioStorageContext>(provider =>
                new AzureBlobContext(configuration.GetConnectionString("StopifyStorage")!, "songs"));
    }
}
