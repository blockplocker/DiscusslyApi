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

        public DbSet<Discussly.Models.ForumCategory> ForumCategory { get; set; } = default!;
        public DbSet<Discussly.Models.Post> Post { get; set; } = default!;
    }
}
