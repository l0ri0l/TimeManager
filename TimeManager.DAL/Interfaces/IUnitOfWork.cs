using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.DAL.Entities;

namespace TimeManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<ProjectTask> Tasks { get; }
        IRepository<TaskComments> TaskComments { get; }
        void Save();
    }
}
