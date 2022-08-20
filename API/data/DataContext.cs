using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.entities;
using Microsoft.EntityFrameworkCore;

namespace API.data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<entities.Task> Tasks { get; set; }
    }
}