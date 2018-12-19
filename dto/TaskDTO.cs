using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.dto
{
    public class TaskDTO
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public long CategoryID { get; set; }
        public long CreatedAt { get; set; }
        public bool isDone { get; set; }
        public long ExpiredAt { get; set; }
        public long UserId { get; set; }
    }
}
