using Microsoft.EntityFrameworkCore;
using My_Family.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace My_Family.Context
{
    public class AppDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server = localhost;Port = 5432;User id = postgres; Password = dotnet;Database = family");
        }
        public virtual DbSet<Login>? login { get; set; }
        public virtual DbSet<Costs>? cost { get; set; }

       
    }
}
