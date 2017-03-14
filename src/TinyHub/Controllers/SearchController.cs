using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;
using TinyHub.ViewModels;

namespace TinyHub.Controllers
{
    public class SearchController : Controller
    {
        private IThRepository _repository;

        public SearchController(IThRepository repository)
        {
            _repository = repository;
        }
        
        public IActionResult SearchProject(string projectName)
        {
            var model = _repository.GetAllProjectsContainingName(projectName);
            var projects = Mapper.Map<IEnumerable<ProjectViewModel>>(model);

            return View(projects);
        }

        public IActionResult SearchAdvanced()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdvancedSearchResult(AdvancedSearchProjectViewModel vm)
        {
            var results = _repository.GetAllProjectsInAdvancedSearch(vm.ProjectName, 
                vm.FromUser, vm.DateCreatedFrom, vm.DateCreatedTo, vm.License);

            return View(results);
        }
    }
}
