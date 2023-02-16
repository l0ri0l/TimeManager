using System.Data.Entity;
using TimeManager.DAL.Entities;

namespace TimeManager.DAL.EF
{
    public class TasksContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<TaskComments> TaskComments { get; set; }

        public TasksContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new TimeManagerInitializer());
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
