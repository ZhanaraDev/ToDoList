using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using WebApplication1.dto;
using WebApplication1.Models;
using Task = WebApplication1.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{ 
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        
        private DataContext _context;
        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            var categories = _context.TaskCategory.Where(c => c.User.Id == long.Parse(User.Identity.Name)).ToList();
            return JsonConvert.SerializeObject(categories,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            TaskCategory cat = _context.TaskCategory.Find((long)id);
            return JsonConvert.SerializeObject(cat,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CategoriesDTO cat)
        {
            TaskCategory tc = new TaskCategory {
                CategoryName = cat.Name,
                Description = cat.Description,
                User = _context.User.Find(long.Parse(User.Identity.Name))
            };
            try
            {
                _context.TaskCategory.Add(tc);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public ActionResult<string> Put([FromBody]CategoriesDTO dto)
        {
           TaskCategory category = _context.TaskCategory.Where(tc => tc.TaskCategoryID.Equals(dto.Id)).First();
            
            bool hasChanges = false;

            if (dto.Name != null)
            {
                category.CategoryName = dto.Name;
                hasChanges = true;
            }
            
            if (dto.Description != null)
            {
                category.Description = dto.Description;
                hasChanges = true;
            }
            if (hasChanges)
            {
                _context.TaskCategory.Update(category);
                _context.SaveChanges();
            }
            
            return JsonConvert.SerializeObject(category, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete([BindRequired]long id)
        {
            TaskCategory cat = _context.TaskCategory.Find((long)id);
            if (!cat.CategoryName.Equals("Default") && cat.User.Id.Equals(long.Parse(User.Identity.Name)))
            {
                var tasks = _context.Task.Where(t => t.Category.Equals(cat)).ToList();
                foreach (Task task in tasks)
                {
                    _context.Task.Attach(task);
                    _context.Task.Remove(task);
                }
                _context.TaskCategory.Attach(cat);
                _context.TaskCategory.Remove(cat);
               _context.SaveChanges();      
            }
         
        }
    }
}
