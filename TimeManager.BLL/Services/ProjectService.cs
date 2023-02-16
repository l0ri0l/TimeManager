using AutoMapper;
using TimeManager.BLL.DTO;
using TimeManager.BLL.Infrastruction;
using TimeManager.BLL.Interfaces;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.BLL.Services
{
    public class ProjectService : IProjectService
    {
        IUnitOfWork DataBase { get; set; }



        public ProjectService(IUnitOfWork uow)
        {
            DataBase = uow;
        }

        public ProjectDTO GetProject(Guid Id)
        {
            try
            {
                var project = DataBase.Projects.Get(Id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
                return mapper.Map<Project, ProjectDTO>(project);
            }
            catch 
            {
                return null;
            } 
        }

        public void AddProject(ProjectDTO projectDTO)
        {
            Project project = new Project
            {
                Id = projectDTO.Id,
                ProjectName = projectDTO.ProjectName,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            DataBase.Projects.Create(project);
            DataBase.Save();
        }


        public void Dispose()
        {
            DataBase.Dispose();
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var smth = DataBase.Projects.GetAll().ToList();
            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            var smthMapped = mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(DataBase.Projects.GetAll());

            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(DataBase.Projects.GetAll());
        }

        public ProjectTaskDTO GetTask(Guid id)
        {
            if (id == null)
            {
                throw new ValidationExeption("Project task id is not defined", "");
            }
            var task = DataBase.Tasks.Get(id);
            if (task == null)
            {
                throw new ValidationExeption("Project task is not found", "");
            }
            return new ProjectTaskDTO 
            {
                Id = task.Id, 
                ProjectId = task.ProjectId, 
                CancelDate = task.CancelDate, 
                CreateDate = task.CreateDate, 
                StartDate = task.StartDate, 
                TaskName = task.TaskName, 
                UpdateDate = task.UpdateDate 
            };
        }

        public IEnumerable<ProjectTaskDTO> GetAllTasks(Guid ProjectId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTask, ProjectTaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectTask>, List<ProjectTaskDTO>>(DataBase.Tasks.Find(t => t.ProjectId == ProjectId));
        }

        public void RemoveProject(Guid id)
        {
            var projectTasks = GetAllTasks(id);
            foreach (var task in projectTasks)
            {
                DataBase.Tasks.Delete(task.Id);
            }

            DataBase.Projects.Delete(id);
            DataBase.Save();
        }

        public void UpdateProject(ProjectDTO projectDTO)
        {
            var project = DataBase.Projects.Get(projectDTO.Id);
            project.ProjectName = projectDTO.ProjectName;
            project.CreateDate = projectDTO.CreateDate;
            project.UpdateDate = projectDTO.CreateDate;
            DataBase.Projects.Update(project);
            DataBase.Save();
        }
    }
}
