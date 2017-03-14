using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;

namespace TinyHub.Controllers
{
    public class HomeController : Controller
    {
        private IThRepository _repository;

        public HomeController(IThRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyProjects()
        {
            var userName = User.Identity.Name;
            var projects = _repository.GetAllProjectsByUser(userName);
            
            return View(projects);
        }

        [Authorize]
        public IActionResult NewProject()
        {
            return View();
        }
    }
}
