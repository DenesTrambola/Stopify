namespace Stopify.Exceptions.ValidationExceptions;

public class EntityNotFoundException : Exception
{
    public string ErrorMessage { get; private set; }

    public EntityNotFoundException(string entityName)
        : base($"{entityName} not found!") =>
        ErrorMessage = $"{entityName}  not found!";
}
