using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.services
{
    public interface IUserProfileService
    {
        void Create(UserProfile u);
    }
    public class UserProfileService : IUserProfileService
    {
        private DataContext _context;

        public UserProfileService(DataContext context)
        {
            _context = context;
        }
        public void Create(UserProfile userProfile)
        {
            _context.UserProfile.Add(userProfile);
            _context.SaveChanges();
        }
    }
}
