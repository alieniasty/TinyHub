using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;

namespace TinyHub.Controllers.API
{
    [Route("/api/notification/")]
    [Authorize]
    public class NotificationController : Controller
    {
        private IThRepository _repository;

        public NotificationController(IThRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("notifications")]
        public IActionResult Notifications(string projectName)
        {
            var notifications = _repository.GetNotificationsByProject(projectName);

            return Ok(notifications);
        }

        [HttpDelete("notificationdelete/{id}")]
        public IActionResult NotificationDelete()
        {
            var notificationId = RouteData.Values["id"].ToString();

            if (_repository.DeleteNotification(int.Parse(notificationId)))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
