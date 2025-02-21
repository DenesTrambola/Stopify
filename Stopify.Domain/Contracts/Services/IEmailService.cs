namespace Stopify.Domain.Contracts.Services;

public interface IEmailService
{
    void SendVerificationCode(string email, string verificationCode);
    void VerifyCode(string verificationCode);
    void ProcessVerificationCode(string email);
}
