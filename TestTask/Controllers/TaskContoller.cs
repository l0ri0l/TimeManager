using Microsoft.AspNetCore.Mvc;
using System;
using TimeManager.BLL.Interfaces;

namespace TimeManager.WEB.Controllers
{
    public class TaskContoller : Controller
    {
        private ITaskService taskService;

        public TaskContoller(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public ActionResult GetTask(Guid Id)
        {

            return View();
        }
    }
}
