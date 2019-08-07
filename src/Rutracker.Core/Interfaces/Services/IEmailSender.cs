﻿using System.Threading.Tasks;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}