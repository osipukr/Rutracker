﻿using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryCreateView : View
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}