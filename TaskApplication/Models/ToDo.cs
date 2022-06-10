using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskApplication.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Display(Name = "Название задачи")]
        public string TaskName { get; set; }
        [Display(Name = "Статус задачи")]
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public string WhoIs { get; set; }
        [ForeignKey("WhoIs")]
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Дата создание задачи")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfCreateTask { get; set; }
    }
}