namespace Stopify.Exceptions.ValidationExceptions;

public class EntityAlreadyExistsException : Exception
{
    public string ErrorMessage { get; private set; }

    public EntityAlreadyExistsException(string entityName)
        : base($"{entityName} already exists!") =>
        ErrorMessage = $"{entityName} already exists!";
}
