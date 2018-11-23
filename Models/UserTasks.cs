using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserTasks
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long TaskId { get; set; }
        public Task Task { get; set; }

    }
}
