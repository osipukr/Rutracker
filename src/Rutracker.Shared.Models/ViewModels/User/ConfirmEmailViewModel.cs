﻿using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}