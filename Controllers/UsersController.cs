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
using WebApplication1.dto;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IUserProfileService _userProfileService;
        private readonly AppSettings _appSettings;
        public UsersController(IUserService userService, IUserProfileService userProfileService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _appSettings = appSettings.Value;
        }
        

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
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
                UserProfile up = new UserProfile { User = u,Role=1 };//cuz its ordinary user
                _userProfileService.Create(up);

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