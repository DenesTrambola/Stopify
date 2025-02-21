namespace Stopify.Domain.Other;

public static class EmailValidation
{
    public static bool CheckFormat(string email) =>
        !string.IsNullOrWhiteSpace(email) && System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}
