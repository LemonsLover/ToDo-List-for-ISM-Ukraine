using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ToDoListContext db;
        

        public object Properties { get; private set; }

        public ToDoListController(ToDoListContext context)
        {
            this.db = context;
        }

        public async Task<ActionResult> Index()
        {
            IQueryable<ToDoListModel> tasks = from i in db.ToDoList orderby i.Id select i;

            List<ToDoListModel> toDoList = await tasks.ToListAsync();
            return View(toDoList);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddTask(ToDoListModel task)
        {
            if (ModelState.IsValid)
            {
                db.Add(task);
                await db.SaveChangesAsync();

                TempData["Success"] = "Task was added to list !";
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ToDoListModel task = await db.ToDoList.FindAsync(id);

            if(task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ToDoListModel task)
        {
            if (ModelState.IsValid)
            {
                db.Update(task);
                await db.SaveChangesAsync();
                TempData["Success"] = "Task was updated !";
                return RedirectToAction("Index");
            }
            return View(task);
        }
        public async Task<ActionResult> Delete(int id)
        {
            ToDoListModel task = await db.ToDoList.FindAsync(id);
            if (task == null)
            {
                TempData["Error"] = "Task you choose doesn't exist !";
            }
            else
            {
                db.Remove(task);
                await db.SaveChangesAsync();

                TempData["Success"] = "Task was deleted !";
            }
            return RedirectToAction("Index");
        }
    }
}
