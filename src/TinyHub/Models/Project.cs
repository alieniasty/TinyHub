using System;
using System.Collections.Generic;

namespace TinyHub.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsPrivateProject { get; set; }

        public LicenseType License { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<ProjectNotification> ProjectNotifications { get; set; }
    }
}

