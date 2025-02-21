namespace Stopify.Domain.Contracts.Other;

public interface IEmailSender
{
    Task SendEmailAsync(string recipient, string subject, string body);
}
