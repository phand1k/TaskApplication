using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskApplication.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Display(Name="Статус задачи")]
        public string Name { get; set; }
        public ICollection<ToDo> ToDos { get; set; }
        public Status()
        {
            ToDos = new List<ToDo>();
        }
    }
}