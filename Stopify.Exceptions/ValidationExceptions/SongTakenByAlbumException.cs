namespace Stopify.Exceptions.ValidationExceptions;

public class SongTakenByAlbumException : Exception
{
    public string ErrorMessage { get; private set; }

    public SongTakenByAlbumException()
        : base("This song is already taken by an album!") =>
        ErrorMessage = "This song is already taken by an album!";
}
