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
        public String TaskName { get; set; }
        public String TaskDesription { get; set; }
        [ForeignKey("TaskCategoryID")]
        public long TaskCategoryID { get; set; }
        public TaskCategory TaskCategory { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
        public bool isImportant { get; set; }
        public virtual ICollection<UserTasks> UserTasks { get; set; }


    }
}
