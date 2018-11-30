using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.dto
{
    public class TaskDTO
    {
        public String TaskName { get; set; }
        public String TaskDesription { get; set; }
        public long CategoryID { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
        public bool isImportant { get; set; }
    }
}
