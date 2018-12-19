using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        
        private DataContext _context;
        public UserProfileController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public string Get()
        {
            var userProfile = _context.UserProfile.Where(profile =>  profile.User.Id == long.Parse(User.Identity.Name)).First();
            return JsonConvert.SerializeObject(userProfile,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody] UserProfileDTO dto)
        {
            UserProfile userProfile = _context.UserProfile.Where(up => up.User.Id == long.Parse(User.Identity.Name)).First();
          
            bool hasChanges = false;
            if (dto.Name != null)
            {
                userProfile.Name = dto.Name;
                hasChanges = true;
            }

            if (dto.Surname != null)
            {
                userProfile.Surname = dto.Surname;
                hasChanges = true;
            }

            if (hasChanges)
            {
                _context.UserProfile.Update(userProfile);
                _context.SaveChanges();
            }
            
            return JsonConvert.SerializeObject(userProfile, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }
        
    }
}