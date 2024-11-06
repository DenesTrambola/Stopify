namespace Stopify.Domain.Contracts.Services;

public interface IEmailService
{
    void VerifyCode(string recipient, string subject, string body);
}
