namespace Stopify.Domain.Other;

public static class CodeGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateVerificationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(0, 6)
            .Select(_ => chars[_random.Next(chars.Length)])
            .ToArray());
    }
}
