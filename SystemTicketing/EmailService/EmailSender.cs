using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using SystemTicketing.EmailService;
using SystemTicketing.EmailService.EmailConfig;

namespace SystemTicketing.EmailService { 
public class EmailSender : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    // استخدم IOptions<EmailConfiguration> بدلاً من EmailConfiguration مباشرة
    public EmailSender(IOptions<EmailConfiguration> emailConfig)
    {
        _emailConfig = emailConfig.Value; // احصل على القيمة من IOptions
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAddress));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using var client = new SmtpClient();

        client.Timeout = 50000; 
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, true);
            await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        
    }
}}