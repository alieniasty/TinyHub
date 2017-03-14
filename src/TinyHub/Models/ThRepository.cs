using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TinyHub.ViewModels;

namespace TinyHub.Models
{
    public class ThRepository : IThRepository
    {
        private TinyHubContext _context;

        public ThRepository(TinyHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAllProjectsByUser(string userName)
        {
            return _context.Projects
                .Where(n => n.UserName == userName)
                .ToList();
        }

        public void AddProject(Project newProject)
        {
            _context.Projects.Add(newProject);
            _context.SaveChangesAsync();
        }

        public void AddBug(Bug newBug, string identityName)
        {
            var project = _context.Projects
                .Include(n => n.Bugs)
                .Include(n => n.ProjectNotifications)
                .FirstOrDefault(n => n.Name == newBug.FromProject);

            Notify(identityName, project.Name, NotificationType.BugCreated);

            project.Bugs.Add(newBug);
            _context.Bugs.Add(newBug);
            _context.SaveChangesAsync();
        }

        public void UpdateBug(BugUpdateViewModel vm, string identityName)
        {
            var bug =
                _context.Bugs.FirstOrDefault(
                    n =>
                        n.Description == vm.Description && 
                        n.FromProject == vm.FromProject &&
                        n.UserCalling == vm.UserCalling);

            bug.Description = vm.NewDescription;

            Notify(identityName, bug.FromProject, NotificationType.BugUpdated);

            _context.Bugs.Update(bug);
            _context.SaveChanges();
        }

        public void NotifyOfFileUploaded(string projectName, string identityName)
        {
            Notify(identityName, projectName, NotificationType.FileUploaded);
            _context.SaveChangesAsync();
        }

        public void NotifyOfFileDeleted(string projectName, string identityName)
        {
            Notify(identityName, projectName, NotificationType.FileDeleted);
            _context.SaveChangesAsync();
        }

        public bool DeleteNotification(int notificationId)
        {
            var notification = _context.ProjectNotifications
                .FirstOrDefault(n => n.Id == notificationId);

            if (notification == null)
            {
                return false;
            }

            _context.Remove(notification);
            _context.SaveChanges();

            return true;
        }

        public Project GetSingleProject(string name)
        {
            return _context.Projects
                .FirstOrDefault(n => n.Name == name);
        }

        public IEnumerable<Bug> GetAllBugs(string projectName)
        {
            return _context.Bugs
                .Where(b => b.FromProject == projectName)
                .ToList();
        }

        public IEnumerable<Project> GetAllProjectsContainingName(string projectName)
        {
            var x = _context.Projects
                .Where(n => n.Name.ToLower().Contains(projectName) && n.IsPrivateProject == false)
                .ToList();

            return x;
        }

        public IEnumerable<Project> GetAllProjectsInAdvancedSearch(string vmProjectName, string vmFromUser, DateTime? vmDateCreatedFrom,
            DateTime? vmDateCreatedTo, LicenseType? vmLicense)
        {
            IQueryable<Project> query = _context.Projects;

            if (!string.IsNullOrEmpty(vmProjectName))
            {
                query = query.Where(p => p.Name.Contains(vmProjectName));
            }

            if (!string.IsNullOrEmpty(vmFromUser))
            {
                query = query.Where(p => p.UserName.Contains(vmFromUser));
            }

            if (vmLicense.HasValue)
            {
                query = query.Where(p => p.License == vmLicense);
            }

            if (vmDateCreatedFrom.HasValue)
            {
                query = query.Where(p => p.DateCreated >= vmDateCreatedFrom);
            }

            if (vmDateCreatedTo.HasValue)
            {
                query = query.Where(p => p.DateCreated <= vmDateCreatedTo);
            }

            query = query.Where(p => p.IsPrivateProject == false);

            return query.ToList();
        }

        public IEnumerable<ProjectNotification> GetNotificationsByProject(string projectName)
        {
            var notifications = _context.ProjectNotifications
                .Where(n => n.ProjectId.Name == projectName);

            return notifications;
        }

        public bool DeleteProject(string projectName)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Name == projectName);
            if (project == null)
            {
                return false;
            }

            _context.Remove(project);
            _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteSingleBug(string description, string identityName)
        {
            var bug = _context
                .Bugs
                .FirstOrDefault(d => d.Description == description);

            if (bug == null)
            {
                return false;
            }

            Notify(identityName, bug.FromProject, NotificationType.BugDeleted);

            _context.Bugs.Remove(bug);
            _context.SaveChangesAsync();
            return true;
        }

        private void Notify(string identity, string projectName, NotificationType type)
        {
            var project = _context.Projects
                .Include(n => n.ProjectNotifications)
                .FirstOrDefault(n => n.Name == projectName);

            var time = DateTime.ParseExact(DateTime.Now.ToString("f"), "f", null);

            var notification = new ProjectNotification()
            {
                User = identity,
                ProjectId = project,
                TimeOfAppear = time,
                Type = type
            };

            project.ProjectNotifications.Add(notification);
            _context.ProjectNotifications.Add(notification);
        }
    }
}
