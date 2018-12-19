using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApplication1.dto;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.services;
using Task = WebApplication1.Models.Task;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IUserProfileService _userProfileService;
        private readonly AppSettings _appSettings;
        private DataContext _context;

        public UsersController(IUserService userService, IUserProfileService userProfileService, IOptions<AppSettings> appSettings, DataContext context)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _appSettings = appSettings.Value;
            _context = context;
        }
        

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var users = _context.User.ToList();
            return JsonConvert.SerializeObject(users,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
           User user = _context.User.Where(us => us.Id.Equals(id)).First();
            
            return JsonConvert.SerializeObject(user);
        }

        // POST api/values
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegistrationReq req)
        {
            User u = new User{ UserName=req.UserName, Password=req.Password};
            Trace.WriteLine(u.Password,u.UserName);
          
            try
            {
                User user = _userService.Create(u, req.Password);
                UserProfile up = new UserProfile { User = u,Role=1 };//cuz its ordinary user
                _userProfileService.Create(up);

                //here we need to create a default task category
                TaskCategory tc = new TaskCategory {
                    CategoryName = "Default",
                    Description = "This category is default",
                    User = user
                };
                _context.TaskCategory.Add(tc);
                _context.SaveChanges();
               
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
            Trace.WriteLine("--auth--");
            var user = _userService.Authenticate(req.UserName, req.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {Token=tokenString});
        }

     
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           var categories = _context.TaskCategory.Where(tc => tc.User.Id.Equals(id)).ToList();
            foreach (TaskCategory cat in categories)
            {
                var tasks = _context.Task.Where(t => t.Category.TaskCategoryID.Equals(cat.TaskCategoryID)).ToList();
                foreach (Task task in tasks)
                {
                    _context.Task.Attach(task);
                    _context.Task.Remove(task);
                }
                _context.TaskCategory.Attach(cat);
                _context.TaskCategory.Remove(cat);
                _context.SaveChanges();
            }

            UserProfile userProfile = _context.UserProfile.Where(up => up.User.Id.Equals(id)).First();
            _context.UserProfile.Remove(userProfile);
            User user = _context.User.Where(u => u.Id.Equals(id)).First();
            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }
}