using System;
using System.Collections.Generic;
using System.IO;
using TinyHub.ViewModels;

namespace TinyHub.Models
{
    public interface IThRepository
    {
        void AddProject(Project newProject);

        void AddBug(Bug newBug, string identityName);

        void UpdateBug(BugUpdateViewModel vm, string identityName);

        void NotifyOfFileUploaded(string projectName, string identityName);

        void NotifyOfFileDeleted(string projectName, string identityName);

        bool DeleteSingleBug(string description, string identityName);

        bool DeleteProject(string projectName);

        bool DeleteNotification(int notificationId);

        Project GetSingleProject(string name);

        IEnumerable<Project> GetAllProjectsByUser(string userName);

        IEnumerable<Bug> GetAllBugs(string projectName);

        IEnumerable<Project> GetAllProjectsContainingName(string projectName);

        IEnumerable<Project> GetAllProjectsInAdvancedSearch(string vmProjectName, string vmFromUser, 
            DateTime? vmDateCreatedFrom, DateTime? vmDateCreatedTo, LicenseType? vmLicense);

        IEnumerable<ProjectNotification> GetNotificationsByProject(string projectName);
    }
}