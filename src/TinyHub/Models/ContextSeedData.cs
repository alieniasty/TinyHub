using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TinyHub.Models
{
    public class ContextSeedData
    {
        private TinyHubContext _context;
        private UserManager<TinyHubUser> _manager;

        public ContextSeedData(TinyHubContext context, UserManager<TinyHubUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task EnsureSeedData()
        {
            if (await _manager.FindByEmailAsync("test@domain.com") == null)
            {
                var user = new TinyHubUser()
                {
                    UserName = "Test1",
                    Email = "test@domain.com"
                };

                await _manager.CreateAsync(user, "P@ssw0rd!!!%");
                
            }

            if (!_context.Projects.Any())
            {
                var initialProject = new Project()
                {
                    DateCreated = DateTime.Now,
                    IsPrivateProject = false,
                    Name = "School project No.1",
                    UserName = "Test1",
                    Bugs = new List<Bug>
                    {
                        new Bug() { Description = "Test description", FromProject = "School project No.1", Priority = PriorityType.Urgent, UserCalling = "Test1"}
                    }
                };

                _context.Projects.Add(initialProject);

                var initiajProjectNo2 = new Project()
                {
                    DateCreated = DateTime.Now,
                    IsPrivateProject = true,
                    Name = "School project No.2",
                    UserName = "Test1",
                    Bugs = new List<Bug>
                    {
                        new Bug() { Description = "Test description 2", FromProject = "School project No.2", Priority = PriorityType.High, UserCalling = "Test1"}
                    }
                };

                _context.Projects.Add(initiajProjectNo2);

                await _context.SaveChangesAsync();
            }
        }
    }
}
