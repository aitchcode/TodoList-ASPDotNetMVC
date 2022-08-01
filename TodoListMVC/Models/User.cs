using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoListMVC.Models
{
    public class User
    {
        private static int id = 0;
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public User() { }
        public User(string name)
        {
            Id = ++id;
            Name = name;
        }
    }
}