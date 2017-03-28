using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavigationMenu.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int Order { get; set; }
    }
}