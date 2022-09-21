using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyDbContext
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions<MyDb> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
