using DataAccess.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Core
{
    public class DataContext : DbContext
    {
        public DbSet<CourseDbModel> CoursesEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = TrainerConcept; Trusted_Connection = True;");
        }
    }
}
