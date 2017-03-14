using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TinyHub.Models
{
    public class TinyHubContext : IdentityDbContext<TinyHubUser>
    {
        public TinyHubContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectNotification> ProjectNotifications { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Filename=./TinyHubDatabase.db");
        }
    }
}
