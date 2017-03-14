using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinyHub.Models;

namespace TinyHub.ViewModels
{
    public class ProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        
        public string UserName { get; set; }

        public DateTime DateCreated = DateTime.Now;

        [Required]
        public bool IsPrivateProject { get; set; }

        [Required]
        public LicenseType License { get; set; }
    }
}
