using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserProfile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }       //0 - admin, 1 - user
        public long UserRef { get; set; }
        public virtual User user { get; set; }
    }
}
