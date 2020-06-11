using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        { }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ItemsModel> ItemsModel { get; set; }
        public DbSet<SizeModel> Size { get; set; }
        public DbSet<ColorModel> Color { get; set; }
    }


}
