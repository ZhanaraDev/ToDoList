using System;
using System.Diagnostics;
using System.Linq;
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
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        
        [HttpGet]
        [Route("current")]
        public string GetNotFinished()
        {
            long CurretUserId = long.Parse(User.Identity.Name);
            
            var tasks = _context.Task.Where(t => t.UserTasks.Any(ut => ut.UserId == CurretUserId)).Where(t => t.isDone.Equals(false)).ToList();

            return JsonConvert.SerializeObject(tasks,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
        
        [HttpGet]
        [Route("finished")]
        public string GetFinished()
        {
            long CurretUserId = long.Parse(User.Identity.Name);
            
            var tasks = _context.Task.Where(t => t.UserTasks.Any(ut => ut.UserId == CurretUserId)).Where(t => t.isDone.Equals(true)).ToList();

            return JsonConvert.SerializeObject(tasks,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        [HttpGet]
        [Route("category/{cat_id:int}")]
        public string GetByCategory(int cat_id)
        {
         
            long CurrentUserId = long.Parse(User.Identity.Name);
            var tasks = _context.Task.Where(t => t.UserTasks.Any(ut => ut.UserId == CurrentUserId) && 
                        t.Category.TaskCategoryID == (long)cat_id).ToList();
            Trace.WriteLine(tasks);
            return JsonConvert.SerializeObject(tasks,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Task task = _context.Task.Find((long)id);
            return JsonConvert.SerializeObject(task); 
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TaskDTO _task)
        {
            Task task = new Task {
                Name = _task.Name,
                Description = _task.Description,
                Category = _context.TaskCategory.Find(_task.CategoryID),
                CreatedAt = _task.CreatedAt
            };

            if (_task.ExpiredAt != null)
            {
                task.ExpiredAt = _task.ExpiredAt;
            }
            
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
        public ActionResult<string> Put(int id, [FromBody]TaskDTO dto)
        {
           Task task = _context.Task.Where(t => t.Id.Equals(id)).First();
            bool hasChanges = false;
            
            if (dto.Name != null)
            {
                task.Name = dto.Name;
                hasChanges = true;
            }

            if (dto.Description != null)
            {
                task.Description = dto.Description;
                hasChanges = true;
            }

            if (hasChanges)
            {
                _context.Task.Update(task);
                _context.SaveChanges();
            }
            
            return JsonConvert.SerializeObject(task, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Task task = new Task { Id = id };
            _context.Task.Attach(task);
            _context.Task.Remove(task);
            _context.SaveChanges();
        }
    }
}
