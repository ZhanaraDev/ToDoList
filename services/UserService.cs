using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.services
{
    public interface IUserService
    {
        User Create(User u, string password);
        object GetById(long userId);
        object Authenticate(string userName, string password);
    }
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public object Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;
            
            // check if password is correct
            var hashedPassword = Convert.ToBase64String(encrypt_password(password));
            var user = _context.User.SingleOrDefault(x => x.UserName == userName && x.Password == hashedPassword);

            if (user == null)
                return null;

            return user;
        }

        public User Create(User u, string password)
        {
            
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("password is required");
            if(_context.User.Any(x => x.UserName == u.UserName))
                throw new ApplicationException("username exists");

            var sha1password = encrypt_password(u.Password);

            u.Password = Convert.ToBase64String(sha1password);
            _context.User.Add(u);
            _context.SaveChanges();
            return u;
        }

        private byte[] encrypt_password(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(password);
            var sha1password = sha1.ComputeHash(data);

            return sha1password;
        }

        public object GetById(long id)
        {
            return _context.User.Find(id);
        }
    }
}
