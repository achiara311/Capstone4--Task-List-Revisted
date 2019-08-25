using System;
using System.Collections.Generic;

namespace TaskListCaptsone6.Models
{
    public partial class Tasks
    {
        public int TasksId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Complete { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
