using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Discussly.Models;

namespace DiscusslyApi.Data
{
    public class DiscusslyApiContext : DbContext
    {
        public DiscusslyApiContext (DbContextOptions<DiscusslyApiContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Post> Post { get; set; }
    }
}
