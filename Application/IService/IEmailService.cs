﻿namespace Application.IService
{
    public interface IEmailService
    {
        Task  SendEmailAsync(string toEmail, string subject, string message);
    }
}
