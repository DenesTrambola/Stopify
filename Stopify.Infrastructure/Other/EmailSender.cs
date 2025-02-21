using Stopify.Domain.Contracts.Other;
using System.Net.Mail;

namespace Stopify.Infrastructure.Other;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;

    public EmailSender(SmtpClient smtpClient) =>
        _smtpClient = smtpClient;

    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress("tramboladenes@gmail.com");
        mail.To.Add(recipient);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = false;

        await _smtpClient.SendMailAsync(mail);
    }
}
