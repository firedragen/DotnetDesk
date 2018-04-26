﻿using Microsoft.AspNetCore.Identity;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IDotnetdesk
    {
        Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

    }
}
