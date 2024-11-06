using Stopify.Domain.Contracts.Other;
using Stopify.Domain.Contracts.Services;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Other;

public class EmailService : IEmailService
{
    private readonly IEmailSender _sender;

    public EmailService(IEmailSender sender) =>
        _sender = sender;

    public void VerifyCode(string recipient, string subject, string body)
    {
        _sender.SendEmailAsync(recipient, subject, body);
        string activationCode = "3412";
        Console.Write("Enter your Accout Activation Code: ");
        string codeInput = Console.ReadLine();
        if (!string.Equals(codeInput.Trim().Replace(" ", ""), activationCode, StringComparison.OrdinalIgnoreCase))
            throw new InvalidActivationCodeException();
    }
}
