using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.dto;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Console.WriteLine("lalalal");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegistrationReq req)
        {
            Trace.WriteLine("hey",req.UserName);
            User u = new User{ UserName=req.UserName, Password=req.Password};
            Trace.WriteLine(u.Password,u.UserName);
          
            try
            {
                _userService.Create(u, req.Password);
                return Ok();
            }
            catch(ApplicationException ex)
            {
                return BadRequest(new {message =ex.Message });
            }
        }

        [HttpPost]
        [Route("auth")]
        public IActionResult Authenticate([FromBody] RegistrationReq req)
        {
            var user = _userService.Authenticate(req.UserName, req.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}