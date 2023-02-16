using Ninject.Modules;
using TimeManager.BLL.Interfaces;
using TimeManager.BLL.Services;

namespace TimeManager.WEB.Util
{
    public class InjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
            Bind<ITaskService>().To<TaskService>();
        }
    }
}
