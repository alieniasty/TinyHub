using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TinyHub.Models
{
    public class TinyHubUser : IdentityUser
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}
