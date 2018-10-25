using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TaskCategory
    {
        public long TaskCategoryID { get; set; }
        public String CategoryName { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
