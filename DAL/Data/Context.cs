using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Film> Films { get; set; }

    }
}
