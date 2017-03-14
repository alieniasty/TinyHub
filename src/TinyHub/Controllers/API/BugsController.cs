using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;
using TinyHub.ViewModels;

namespace TinyHub.Controllers.API
{
    [Route("/api/bugs/")]
    [Authorize]
    public class BugsController : Controller
    {
        private IThRepository _repository;

        public BugsController(IThRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("createbug")]
        public IActionResult CreateBug([FromBody]Bug model)
        {
            model.UserCalling = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _repository.AddBug(model, User.Identity.Name);
                return Created($"api/project/createbug/{model.Id}", model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("deletebug/{description}")]
        public IActionResult DeleteBug(string description)
        {
            if (_repository.DeleteSingleBug(description, User.Identity.Name))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getbugs")]
        public IActionResult GetBugs(string projectName)
        {
            var bugModel = _repository.GetAllBugs(projectName);

            return Ok(bugModel);
        }

        [HttpPost("updatebug")]
        public IActionResult UpdateBug([FromBody]BugUpdateViewModel vm)
        {
            _repository.UpdateBug(vm, User.Identity.Name);
            return Created($"api/project/updatebug/{vm.Description}", vm);
        }
    }
}
