using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoListMVC.Models
{
    public class ListItem
    {
        private static int id = 0;
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Text { get; set; }
        public bool CheckedStatus { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public ListItem() { }
        public ListItem(int userId, string text)
        {
            Id = ++id;
            UserId = userId;
            Text = text;
            CheckedStatus = false;
        }
    }

}