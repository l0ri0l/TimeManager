using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Infrastruction;
using TimeManager.BLL.Interfaces;
using TimeManager.WEB.Models;

namespace TimeManager.WEB.Controllers
{
    public class TaskCommentsController : Controller
    {
        private readonly ITaskService taskService;

        public TaskCommentsController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public JsonResult GetComment(Guid commentId)
        {
            var comment = taskService.GetComment(commentId);
            if (comment == null)
                throw new ValidationExeption("No comment", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskCommentsDTO, TaskCommentsViewModel>()).CreateMapper();
            var commentViewModel = mapper.Map<TaskCommentsDTO, TaskCommentsViewModel>(comment);

            return Json(commentViewModel);
        }

        [HttpPost]
        public JsonResult AddComment(byte CommentType, string CommentContent, Guid taskId)
        {
            var task = taskService.FindTask(taskId);
            if (task == null)
                throw new ValidationExeption("No task", "");

            Guid commentId = Guid.NewGuid();

            var comment = new TaskCommentsDTO()
            {
                Id = commentId,
                TaskId = taskId,
                CommentType = CommentType,
                Content = CommentContent
            };

            taskService.AddComment(comment, taskId);
            
            var commentDTO = taskService.GetComment(commentId);
            if (commentDTO == null)
                throw new ValidationExeption("No comment", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskCommentsDTO, TaskCommentsViewModel>()).CreateMapper();
            var commentViewModel = mapper.Map<TaskCommentsDTO, TaskCommentsViewModel>(commentDTO);

            return Json(commentViewModel);
        }

        [HttpPost]
        public JsonResult EditComment(byte CommentType, string CommentContent, Guid commentId)
        {
            var commentDTO = taskService.GetComment(commentId);
            if (commentDTO == null)
                throw new ValidationExeption("No comment", "");

            var task = taskService.FindTask(commentDTO.TaskId);
            if (task == null)
                throw new ValidationExeption("No task", "");

            commentDTO.Content = CommentContent;
            commentDTO.CommentType = CommentType;

            taskService.EditComment(commentDTO);

            var updatedCommentDTO = taskService.GetComment(commentId);
            if (updatedCommentDTO == null)
                throw new ValidationExeption("No comment", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskCommentsDTO, TaskCommentsViewModel>()).CreateMapper();
            var commentViewModel = mapper.Map<TaskCommentsDTO, TaskCommentsViewModel>(updatedCommentDTO);

            return Json(commentViewModel);
        }

        [HttpDelete]
        public JsonResult RemoveComment(Guid commentId)
        {
            var commentDTO = taskService.GetComment(commentId);
            if (commentDTO == null)
                throw new ValidationExeption("No comment", "");

            var taskId = commentDTO.TaskId;

            taskService.RemoveComment(commentId);

            var updatedTask = taskService.FindTask(taskId); 
            if (updatedTask == null)
                throw new ValidationExeption("No comment", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<ProjectTaskDTO, TaskViewModel>(updatedTask);

            return Json(taskViewModel);
        }
    }
}
