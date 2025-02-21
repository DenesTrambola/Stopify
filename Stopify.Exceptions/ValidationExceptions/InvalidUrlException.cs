namespace Stopify.Exceptions.ValidationExceptions;

public class InvalidUrlException : Exception
{
    public static string ErrorMessage { get; private set; } = "Invalid URL format!";

    public InvalidUrlException() : base(ErrorMessage) { }
}
