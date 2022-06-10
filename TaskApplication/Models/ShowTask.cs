using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace todo.Models
{
    public class ShowTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
    }
}