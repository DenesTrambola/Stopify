using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Infrastructure;

namespace Stopify.ConsoleTest;

// [X] Prevent duplicates for relationship methods in entity services.
// [X] Remake the Create method to Add[..] method in bridge-table services.
// [X] Configure what happens when one of each of entities are removed.
// [X] Fix the RemoveAsync methods so that all foreign keys are removed too with primary key.
// [X] Fill the database and add a few songs into Blob Storage.
// [ ] Add table SongAlbums for tracking song position in a certain album.
// [ ] Remake GetAsync methods and use LINQ method 'Any' to load not all properties but only the must-have ones.

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

        var genreService = provider.GetRequiredService<IGenreService>();
        //await genreService.CreateAsync(new("pop"));
        //await genreService.AddSongAsync("pop", "casa de papel");

        var songService = provider.GetRequiredService<ISongService>();
        //await songService.CreateAsync(new("depicentrum (outro)", 128, new DateOnly(2023, 5, 1), "azahriah_depicentrum"));
        //await songService.RemoveAsync(new("depicentrum (outro)"));
        //await songService.UpdateTitleAsync("hassdalen", "hessdalen");
        //await songService.UpdateAlbumAsync("depicentrum (outro)", "memento");
        //await songService.AddArtistAsync("depicentrum (outro)", "Azahriah");

        var artistService = provider.GetRequiredService<IArtistService>();
        //await artistService.CreateAsync(new("DESH", true, bgImage: "desh"));
        //await artistService.UpdateCountryAsync("Azahriah", "Hungary");
        //await artistService.RemoveAsync(new("Queen Omega"));
        //await artistService.AddSongAsync("Azahriah", "ceremónia");

        var albumService = provider.GetRequiredService<IAlbumService>();
        //await albumService.CreateAsync(new("tripq", new(2023, 5, 1), "azahriah_desh_tripq"));
        //await albumService.RemoveAsync(new("memento"));
        //await albumService.AddSongAsync("tripq", "ceremónia");

        //await PrintSongsByAlbum(songService, 2);
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
