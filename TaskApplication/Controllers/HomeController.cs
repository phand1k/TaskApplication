using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Models;
using todo.Models;

namespace TaskApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            ViewBag.ActiveTasks = context.ToDos.Where(x=>x.WhoIs == currentUserId && x.StatusId == 1).Select(x => new ShowTask()
            {
                Id = x.Id,
                TaskName = x.TaskName,
                Status = x.Status.Name
            });
            ViewBag.CompletedTasks = context.ToDos.Where(x => x.WhoIs == currentUserId && x.StatusId == 2).Select(x => new ShowTask()
            {
                Id = x.Id,
                TaskName = x.TaskName,
                Status = x.Status.Name
            });
            return View();
        }
        [Authorize]
        public ActionResult TaskIsCompleted(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ToDo toDo = context.ToDos.Include(x => x.ApplicationUser).Include(x => x.Status).Where(x => x.Id == id).First();
            if (toDo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.NameTask = toDo.TaskName;
            return View(toDo);
        }
        [HttpPost]
        public ActionResult TaskIsCompleted(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                toDo.TaskName = toDo.TaskName;
                toDo.StatusId = 2;
                context.Entry(toDo).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize]
        public ActionResult DeleteTask(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ToDo toDo = context.ToDos.Include(x=>x.ApplicationUser).Include(x=>x.Status).Where(x=>x.Id == id).First();
            if (toDo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDo);
        }
        [Authorize]
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTaskConfirmed(ToDo toDo)
        {
            context.Entry(toDo).State = EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CreateTask(ToDo task)
        {
            if (ModelState.IsValid)
            {
                task.DateOfCreateTask = DateTime.Now;
                task.WhoIs = User.Identity.GetUserId();
                context.Configuration.ValidateOnSaveEnabled = false;
                task.StatusId = 1;
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize]
        public ActionResult EditTask(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ToDo toDo = context.ToDos.Find(id);
            if (toDo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDo);
        }
        [HttpPost]
        public ActionResult EditTask(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                context.Entry(toDo).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}