﻿namespace Porfolio.Web.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);    
}