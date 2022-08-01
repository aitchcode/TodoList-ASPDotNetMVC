using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoListMVC.Models
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
    }
}