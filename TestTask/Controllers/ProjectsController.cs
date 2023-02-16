using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Interfaces;
using TimeManager.WEB.Models;

namespace TimeManager.WEB.Controllers
{
    public class ProjectsController : Controller
    {
        IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public ActionResult GetAllProjects()
        {
            IEnumerable<ProjectDTO> projectDTOs = projectService.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projects = mapper.Map<IEnumerable<ProjectDTO>, List<ProjectViewModel>>(projectDTOs);

            return View(projects);
        }

        // POST: ProjectContoller/Create
        [HttpPost]
        public ActionResult CreateProject(string projectName)
        {
            var newProject = new ProjectDTO
            {
                ProjectName = projectName,
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            projectService.AddProject(newProject);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectView = mapper.Map<ProjectDTO, ProjectViewModel>(newProject);
            return View(projectView);
        }

        [HttpDelete]
        public ActionResult RemoveProject(Guid Id)
        {
            projectService.RemoveProject(Id);
            return GetAllProjects();
        }

        [HttpGet]
        public ActionResult GetProject(Guid id)
        {
            var projectDTO = projectService.GetProject(id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectView = mapper.Map<ProjectDTO, ProjectViewModel>(projectDTO);
            return View(projectView);
        }

        [HttpPost]
        public ActionResult UpdateProject(ProjectDTO projectData)
        {
            projectService.UpdateProject(projectData);

            return GetProject(projectData.Id);
        }

    }
}
