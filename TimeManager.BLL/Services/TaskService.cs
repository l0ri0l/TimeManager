using AutoMapper;
using System.Text;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Infrastruction;
using TimeManager.BLL.Interfaces;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.BLL.Services
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork DataBase;
        private IProjectService projectService;
        
        public TaskService(IUnitOfWork uow, IProjectService projectService)
        {
            DataBase = uow;
            this.projectService = projectService;
        }

        public void CreateTask(ProjectTaskDTO taskDTO, Guid projectId)
        {
            ProjectTask task = new ProjectTask
            {
                TaskName = taskDTO.TaskName,
                Id = taskDTO.Id,
                CreateDate = taskDTO.CreateDate,
                UpdateDate = taskDTO.UpdateDate,
                StartDate = taskDTO.StartDate,
                CancelDate = taskDTO.CancelDate
            };

            if (projectId == null)
            {
                throw new ValidationExeption("Project id is not defined", "");
            }
            var project = projectService.GetProject(projectId);
            if (project == null)
            {
                throw new ValidationExeption("Project is not found", "");
            }

            task.ProjectId = projectId;
            DataBase.Tasks.Create(task);
            DataBase.Save();

            projectService.UpdateProject(project);
            
        }

        public void AddComment(TaskCommentsDTO commentDTO, Guid taskId)
        {
            var comment = new TaskComments
            {
                Id = commentDTO.Id,
                TaskId = taskId,
                Content = Encoding.UTF8.GetBytes(commentDTO.Content),
                CommentType = commentDTO.CommentType
            };
            DataBase.TaskComments.Create(comment);
            DataBase.Save();
        }

        public void EditComment(TaskCommentsDTO commentDTO)
        {
            var comment = DataBase.TaskComments.Get(commentDTO.Id);
            comment.CommentType = commentDTO.CommentType;
            comment.Content = Encoding.UTF8.GetBytes(commentDTO.Content);
            DataBase.TaskComments.Update(comment);
            DataBase.Save();
        }

        public void RemoveComment(Guid commentId)
        {
            DataBase.TaskComments.Delete(commentId);
            DataBase.Save();
        }

        public ProjectTaskDTO FindTask(Guid Id)
        {
            var task = DataBase.Tasks.Get(Id);
            if (task == null)
                throw new ValidationExeption("No task", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTask, ProjectTaskDTO>()).CreateMapper();
            return mapper.Map<ProjectTask, ProjectTaskDTO>(task);
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public IEnumerable<TaskCommentsDTO> GetComments(Guid taskId)
        {
            if (taskId == null)
            {
                throw new ValidationExeption("Project task id is not defined", "");
            }
            var task = DataBase.Tasks.Get(taskId);
            if (task == null)
            {
                throw new ValidationExeption("Project task is not found", "");
            }

            var comments = DataBase.TaskComments.Find(com => com.TaskId == task.Id);

            var commentsDTO = new List<TaskCommentsDTO>();

            foreach(var com in comments)
            {
                var comDTO = new TaskCommentsDTO()
                {
                    Id = com.Id,
                    TaskId = com.TaskId,
                    CommentType = com.CommentType,
                    Content = Encoding.UTF8.GetString(com.Content)
                };
                commentsDTO.Add(comDTO);
            }

            return commentsDTO;
        }

        public TaskCommentsDTO GetComment(Guid commentId)
        {
            if (commentId == null)
            {
                throw new ValidationExeption("Task comment id is not defined", "");
            }
            var comment = DataBase.TaskComments.Get(commentId);
            if (comment == null)
            {
                throw new ValidationExeption("Task comments is not found", "");
            }

            var commentString = Encoding.UTF8.GetString(comment.Content);

            return new TaskCommentsDTO { Id = comment.Id, TaskId = comment.TaskId, CommentType = comment.CommentType, Content = commentString };
        }

        public void RemoveTask(Guid Id)
        {
            var taskProjectId = DataBase.Tasks.Get(Id)?.ProjectId;
            DataBase.Tasks.Delete(Id);
            DataBase.Save();
            if(taskProjectId != null)
            {
                var project = projectService.GetProject(taskProjectId.Value);
                projectService.UpdateProject(project);
            }
        }

        public void StopTask(Guid taskId)
        {
            if (taskId == null)
            {
                throw new ValidationExeption("Project task id is not defined", "");
            }
            var task = DataBase.Tasks.Get(taskId);
            if (task == null)
            {
                throw new ValidationExeption("Project task is not found", "");
            }

            task.UpdateDate = DateTime.Now;
            task.CancelDate = DateTime.Now;

            DataBase.Tasks.Update(task);
            DataBase.Save();
        }

        public void StartTask(Guid taskId)
        {
            if (taskId == null)
            {
                throw new ValidationExeption("Project task id is not defined", "");
            }
            var task = DataBase.Tasks.Get(taskId);
            if (task == null)
            {
                throw new ValidationExeption("Project task is not found", "");
            }

            task.UpdateDate = DateTime.Now;
            task.StartDate = DateTime.Now;

            DataBase.Tasks.Update(task);
            DataBase.Save();
        }

        public void CancelTask(Guid taskId)
        {
            var task = DataBase.Tasks.Get(taskId);
            if (task == null)
            {
                throw new ValidationExeption("Project task is not found", "");
            }

            StopTask(taskId);

            task.UpdateDate = DateTime.Now;
            task.CancelDate = DateTime.Now;

            DataBase.Tasks.Update(task);
            DataBase.Save();
        }
    }
}
