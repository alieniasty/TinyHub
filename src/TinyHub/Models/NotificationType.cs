using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyHub.Models
{
    public enum NotificationType
    {
        BugUpdated = 1,
        BugCreated = 2,
        BugDeleted = 3,
        FileUploaded = 4,
        FileDeleted = 5
    }
}
