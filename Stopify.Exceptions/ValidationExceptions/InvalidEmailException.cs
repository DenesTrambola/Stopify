namespace Stopify.Exceptions.ValidationExceptions;

public class InvalidEmailException : Exception
{
    public static string ErrorMessage { get; private set; } = "Invalid email format!";

    public InvalidEmailException() : base(ErrorMessage) { }
}
