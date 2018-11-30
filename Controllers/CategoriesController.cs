using System;
using System.Collections.Generic;
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
            return JsonConvert.SerializeObject(cat);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CategoriesDTO cat)
        {
            TaskCategory tc = new TaskCategory {
                CategoryName = cat.CategoryName,
                Description = cat.Description,
                User = _context.User.Find(long.Parse(User.Identity.Name))
            };
            try
            {
                System.Diagnostics.Trace.WriteLine("CREATE CATEGORY");
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TaskCategory cat = new TaskCategory { TaskCategoryID = id };
            _context.TaskCategory.Attach(cat);
            _context.TaskCategory.Remove(cat);
            _context.SaveChanges();
        }
    }
}
