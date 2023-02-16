using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Infrastruction;
using TimeManager.BLL.Interfaces;
using TimeManager.WEB.Models;

namespace TimeManager.WEB.Controllers
{
    public class TaskContoller : Controller
    {
        private IProjectService projectService;
        private ITaskService taskService;

        public TaskContoller(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public JsonResult GetTask(Guid projectId, Guid taskId)
        {
            var projectDTO = projectService.GetProject(projectId);
            if (projectDTO == null)
            {
                throw new ValidationExeption("No project", "");
            }

            var taskDTO = taskService.FindTask(taskId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(taskDTO);

            return Json(taskViewModel);
        }

        [HttpPost]
        public JsonResult AddTask(Guid projectId, string taskName)
        {
            var projectDTO = projectService.GetProject(projectId);
            if (projectDTO == null)
            {
                throw new ValidationExeption("No project", "");
            }

            Guid taskId = Guid.NewGuid();

            var taskDTO = new ProjectTaskDTO()
            {
                ProjectId = projectId,
                TaskName = taskName,
                Id = taskId,
                CreateDate = DateTime.Now
            };

            taskService.CreateTask(taskDTO, projectId);

            var task = taskService.FindTask(taskId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(task);

            return Json(taskViewModel);
        }

        [HttpPost]
        public JsonResult StartTask(Guid taskId)
        {
            var taskDTO = taskService.FindTask(taskId);
            if (taskDTO == null)
                throw new ValidationExeption("No task", "");

            taskService.StartTask(taskId);

            var task = taskService.FindTask(taskId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(task);

            return Json(taskViewModel);
        }

        [HttpPost]
        public JsonResult StopTask(Guid taskId)
        {
            var taskDTO = taskService.FindTask(taskId);
            if (taskDTO == null)
                throw new ValidationExeption("No task", "");

            taskService.StopTask(taskId);

            var task = taskService.FindTask(taskId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(task);

            return Json(taskViewModel);
        }

        [HttpPost]
        public JsonResult CancelTask(Guid taskId)
        {
            var taskDTO = taskService.FindTask(taskId);
            if (taskDTO == null)
                throw new ValidationExeption("No task", "");

            taskService.RemoveTask(taskId);

            var task = taskService.FindTask(taskId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(task);

            return Json(taskViewModel);
        }

        [HttpDelete]
        public JsonResult RemoveTask(Guid taskId)
        {
            var taskDTO = taskService.FindTask(taskId);
            if (taskDTO == null)
                throw new ValidationExeption("No task", "");

            taskService.RemoveTask(taskId);

            var project = projectService.GetProject(taskDTO.ProjectId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectViewModel = mapper.Map<ProjectDTO, ProjectViewModel>(project);

            return Json(projectViewModel);
        }
    }
}
