using Stopify.Domain.Contracts.Other;
using Stopify.Domain.Contracts.Services;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Other;

public class EmailService : IEmailService
{
    private readonly IEmailSender _sender;

    public EmailService(IEmailSender sender) =>
        _sender = sender;

    public void SendVerificationCode(string email, string verificationCode) =>
        _sender.SendEmailAsync(email, "Stopify Account Activation", $"Hey there!\nHere's your Stopify Account Verification Code:\n\n{verificationCode}");

    public void VerifyCode(string verificationCode)
    {
        Console.Write("Enter your Account Verification Code: ");
        string codeInput = Console.ReadLine();
        if (!string.Equals(codeInput.Trim().Replace(" ", ""), verificationCode, StringComparison.OrdinalIgnoreCase))
            throw new InvalidActivationCodeException();
    }

    public void ProcessVerificationCode(string email)
    {
        string verificationCode = CodeGenerator.GenerateVerificationCode();
        SendVerificationCode(email, verificationCode);
        VerifyCode(verificationCode);
    }
}
