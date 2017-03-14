using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyHub.Models
{
    public class ProjectNotification
    {
        public int Id { get; set; }

        public Project ProjectId { get; set; }

        public string User { get; set; }

        public NotificationType Type { get; set; }

        public DateTime TimeOfAppear { get; set; }
    }
}
