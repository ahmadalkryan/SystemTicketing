﻿namespace SystemTicketing.EmailService
{
    public interface IEmailService
    {
        Task  SendEmailAsync(string toEmail, string subject, string message);
    }
}
