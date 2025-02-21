namespace Stopify.Exceptions.ValidationExceptions;

public class InvalidActivationCodeException : Exception
{
    public string ErrorMessage { get; private set; }

    public InvalidActivationCodeException()
        : base("Invalid activation code! Try again!") =>
        ErrorMessage = "Invalid activation code! Try again!";
}
