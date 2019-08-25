using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskListCaptsone6.Models;

namespace TaskListCaptsone6.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskListDbContext _context;
        public HomeController(TaskListDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddTask() //adding book to librarybooks table
        {
            return View();
        }

        [HttpPost] //Identity does SESSIONS FOR US
        public IActionResult AddTask(Tasks newTask)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newTask.UserId = id;
            if (ModelState.IsValid && newTask.Complete == true)
            {
                _context.Tasks.Add(newTask);
                _context.SaveChanges();
                return RedirectToAction("ListOfTasks");
            }
            return RedirectToAction("ListOfTasks"); 
        }
        public IActionResult ListOfTasks()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Tasks> showTask = new List<Tasks>(); //gonna store current users task, a holder
            List<Tasks> taskList = _context.Tasks.ToList(); //creates a list from database
            foreach(var task in taskList)
            {
                if (id == task.UserId)
                {
                    showTask.Add(task);
                }
            }
            return View(showTask);
        }
        public IActionResult UpdateTask(int Id)
        {
            Tasks found = _context.Tasks.Find(Id);
            if (ModelState.IsValid && found != null)
            {

                found.Complete = true;
                _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(found);
                _context.SaveChanges();
                return RedirectToAction("ListTasks");
            }
            else
            {
                return RedirectToAction("UpdateTask");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
