using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Task
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual TaskCategory Category { get; set; }
        public long CreatedAt { get; set; }
        public long ExpiredAt { get; set; }
        public bool isDone { get; set; }
        public virtual ICollection<UserTasks> UserTasks { get; set; }

    }
}
