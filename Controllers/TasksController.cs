using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.dto;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private DataContext _context;

        public TasksController(DataContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            long CurretUserId = long.Parse(User.Identity.Name);
            
            var tasks = _context.Task.Where(t => t.UserTasks.Any(ut => ut.UserId == CurretUserId)).ToList();

            return JsonConvert.SerializeObject(tasks,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        [HttpGet]
        [Route("category/{cat_id:int}")]
        public string GetByCategory(int cat_id)
        {
            Trace.WriteLine("Here we go");
            Trace.WriteLine(cat_id);
            long CurrentUserId = long.Parse(User.Identity.Name);
            var tasks = _context.Task.Where(t => t.UserTasks.Any(ut => ut.UserId == CurrentUserId) && 
                        t.TaskCategory.TaskCategoryID == (long)cat_id).ToList();
            Trace.WriteLine(tasks);
            return JsonConvert.SerializeObject(tasks,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Models.Task task = _context.Task.Find((long)id);
            return JsonConvert.SerializeObject(task); 
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TaskDTO _task)
        {
            System.Diagnostics.Trace.WriteLine("CREATE TASK");
            System.Diagnostics.Trace.WriteLine(_task.isImportant);
            Models.Task task = new Models.Task {
                TaskName = _task.TaskName,
                TaskDesription = _task.TaskDesription,
                TaskCategory = _context.TaskCategory.Find(_task.CategoryID),
                DateAdded = DateTime.Now,
                isImportant = _task.isImportant,
                Deadline = _task.Deadline
            };

            try
            {
                _context.Task.Add(task);
                _context.SaveChanges();
                UserTasks ut = new UserTasks
                {
                    Task = _context.Task.Find(task.Id),
                    User = _context.User.Find(long.Parse(User.Identity.Name))
                };
                _context.UserTasks.Add(ut);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Models.Task task = new Models.Task { Id = id };
            _context.Task.Attach(task);
            _context.Task.Remove(task);
            _context.SaveChanges();
        }
    }
}
