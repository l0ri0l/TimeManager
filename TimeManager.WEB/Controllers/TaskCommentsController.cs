using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Infrastruction;
using TimeManager.BLL.Interfaces;
using TimeManager.WEB.Models;

namespace TimeManager.WEB.Controllers
{
    public class TaskCommentsController : Controller
    {
        private ITaskService taskService;

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
        public JsonResult AddComment(TaskCommentsDTO comment, Guid taskId)
        {
            var task = taskService.FindTask(taskId);
            if (task == null)
                throw new ValidationExeption("No task", "");

            Guid commentId = Guid.NewGuid();
            comment.Id = commentId;
            comment.TaskId = taskId;

            taskService.AddComment(comment, taskId);
            
            var commentDTO = taskService.GetComment(commentId);
            if (commentDTO == null)
                throw new ValidationExeption("No comment", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskCommentsDTO, TaskCommentsViewModel>()).CreateMapper();
            var commentViewModel = mapper.Map<TaskCommentsDTO, TaskCommentsViewModel>(commentDTO);

            return Json(commentViewModel);
        }

        [HttpPost]
        public JsonResult EditComment(TaskCommentsDTO comment)
        {
            var task = taskService.FindTask(comment.TaskId);
            if (task == null)
                throw new ValidationExeption("No task", "");

            var commentDTO = taskService.GetComment(comment.Id);
            if (commentDTO == null)
                throw new ValidationExeption("No comment", "");

            commentDTO.Content = comment.Content;
            commentDTO.CommentType = comment.CommentType;

            taskService.EditComment(commentDTO);

            var updatedCommentDTO = taskService.GetComment(comment.Id);
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
