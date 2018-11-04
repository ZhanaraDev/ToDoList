using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] PasswordEnc { get; set; }
        public virtual UserProfile Profile { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
