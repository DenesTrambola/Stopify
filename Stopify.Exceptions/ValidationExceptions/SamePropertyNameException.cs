namespace Stopify.Exceptions.ValidationExceptions;

public class SamePropertyNameException : Exception
{
    public string ErrorMessage { get; private set; }

    public SamePropertyNameException(string propertyName)
        : base($"Nothing changed: {propertyName} stays the same!") =>
        ErrorMessage = $"Nothing changed: {propertyName} stays the same!";
}
