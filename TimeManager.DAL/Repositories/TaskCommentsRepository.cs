using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.DAL.EF;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.DAL.Repositories
{
    public class TaskCommentsRepository : IRepository<TaskComments>
    {
        private TasksContext db;

        public TaskCommentsRepository(TasksContext context)
        {
            this.db = context;
        }

        public void Create(TaskComments item)
        {
            db.TaskComments.Add(item);
        }

        public void Delete(Guid Id)
        {
            TaskComments taskComments = db.TaskComments.Find(Id);
            if (taskComments != null)
            {
                db.TaskComments.Remove(taskComments);
            }
        }

        public IEnumerable<TaskComments> Find(Func<TaskComments, bool> predicate)
        {
            return db.TaskComments.Where(predicate).ToList();
        }

        public TaskComments Get(Guid Id)
        {
            return db.TaskComments.Find(Id);
        }

        public IEnumerable<TaskComments> GetAll()
        {
            return db.TaskComments;
        }

        public void Update(TaskComments item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
