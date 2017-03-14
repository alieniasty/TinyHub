using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;
using TinyHub.ViewModels;

namespace TinyHub.Controllers.API
{
    [Route("/api/project/")]
    [Authorize]
    public class ProjectController : Controller
    {
        private IThRepository _repository;
        private IHostingEnvironment _env;

        public ProjectController(IThRepository repository, IHostingEnvironment env, UserManager<TinyHubUser> userManager)
        {
            _repository = repository;
            _env = env;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]ProjectViewModel vm)
        {
            vm.UserName = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var newProject = Mapper.Map<Project>(vm);
                _repository.AddProject(newProject);

                return Created($"api/project/create/{vm.Name}", newProject);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("delete/{projectName}")]
        public IActionResult Delete(string projectName)
        {
            if (!_repository.DeleteProject(projectName))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost("{id}")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var projectName = RouteData.Values["id"];
            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, projectName.ToString()));

            var uploads = Path.Combine(_env.WebRootPath, projectName.ToString());

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var filestream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                }
            }

            _repository.NotifyOfFileUploaded(projectName.ToString(), User.Identity.Name);

            return RedirectToAction("Project", "Projects", new { id = projectName});
        }

        [HttpDelete("deletefile/{filename}/{projectfolder}")]
        public IActionResult DeleteFile()
        {
            var fileName = RouteData.Values["filename"];
            var projectName = RouteData.Values["projectfolder"];

            var uploads = Path.Combine(_env.WebRootPath, projectName.ToString());
            var filePath = Path.Combine(uploads, fileName.ToString());

            System.IO.File.Delete(filePath);

            _repository.NotifyOfFileDeleted(projectName.ToString(), User.Identity.Name);

            return Ok();
        }
    }
}
