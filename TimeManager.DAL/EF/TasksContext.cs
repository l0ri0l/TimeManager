using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TimeManager.DAL.Entities;

namespace TimeManager.DAL.EF
{
    public class TasksContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<TaskComments> TaskComments { get; set; }

        static TasksContext()
        {
            Database.SetInitializer<TasksContext>(new TimeManagerInitializer());
        }

        public TasksContext(string connectionString) : base(connectionString)
        {

        }
    }

    public class TimeManagerInitializer : DropCreateDatabaseIfModelChanges<TasksContext>
    {
        protected override void Seed(TasksContext db)
        {
            base.Seed(db);
            db.SaveChanges();
        }
    }
}
