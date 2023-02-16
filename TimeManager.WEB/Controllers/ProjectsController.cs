using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Interfaces;
using TimeManager.WEB.Models;

namespace TimeManager.WEB.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public JsonResult GetAllProjects()
        {
            IEnumerable<ProjectDTO> projectDTOs = projectService.GetAll();
            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projects = mapper.Map<IEnumerable<ProjectDTO>, List<ProjectViewModel>>(projectDTOs);

            return Json(projects);
        }

        [HttpPost]
        public JsonResult CreateProject(string projectName)
        {
            var newProject = new ProjectDTO
            {
                ProjectName = projectName,
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            projectService.AddProject(newProject);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectView = mapper.Map<ProjectDTO, ProjectViewModel>(newProject);
            return Json(projectView);
        }

        [HttpDelete]
        public JsonResult RemoveProject(Guid Id)
        {
            projectService.RemoveProject(Id);
            return GetAllProjects();
        }

        [HttpGet]
        public JsonResult GetProject(Guid id)
        {
            var projectDTO = projectService.GetProject(id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectView = mapper.Map<ProjectDTO, ProjectViewModel>(projectDTO);
            return Json(projectView);
        }

        [HttpPost]
        public JsonResult UpdateProject(ProjectDTO projectData)
        {
            projectService.UpdateProject(projectData);

            return GetProject(projectData.Id);
        }

    }
}
