namespace Stopify.Domain.Other;

public static class UrlValidation
{
    public static bool CheckFormat(string? url) =>
    Uri.TryCreate(url, UriKind.Absolute, out var validatedUrl)
    && (validatedUrl.Scheme == Uri.UriSchemeHttp || validatedUrl.Scheme == Uri.UriSchemeHttps);
}
