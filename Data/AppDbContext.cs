using Blog.Models;
using Blog.Models.Comment;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Post> posts { get; set; }
        public DbSet<MainComment> mainComments { get; set; }
        public DbSet<SubComment> subComments { get; set; }


    }
}
