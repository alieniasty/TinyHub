using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;
using TinyHub.ViewModels;

namespace TinyHub.Controllers
{
    public class ProjectsController : Controller
    {
        private IThRepository _repository;
        private IHostingEnvironment _env;

        public ProjectsController(IThRepository repository, IHostingEnvironment env)
        {
            _repository = repository;
            _env = env;
        }
        
        [Authorize]
        public IActionResult Project()
        {
            var projectName = RouteData.Values["id"];

            var projectModel = _repository.GetSingleProject(projectName.ToString());
            var data = Mapper.Map<ProjectViewModel>(projectModel);
            
            var path = Path.Combine(_env.WebRootPath, projectName.ToString());

            List<string> files = new List<string>();

            if (Directory.Exists(path) && Directory.GetFiles(path).Length != 0)
            {
                var fullPathFiles = Directory.GetFiles(path);
                foreach (var fullPathFile in fullPathFiles)
                {
                    files.Add(Path.GetFileName(fullPathFile));
                }

                ViewBag.Files = files;
            }

            return View(data);
        }

        [Authorize]
        public IActionResult GetFile(string fileName, string projectFolder)
        {
            var fullPath = $"{_env.WebRootPath}\\{projectFolder}\\{fileName}";

            FileStream stream = new FileStream(fullPath, FileMode.Open);
            return File(stream, "application/pdf", fileName);
        }
    }
}
