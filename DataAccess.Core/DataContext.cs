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

        //https://www.codeproject.com/Articles/1156558/ASP-NET-Core-Moving-IdentityDbContext-and-EF-model
        //https://stackoverflow.com/questions/41627510/move-identity-to-a-class-library-asp-net-core
        //https://code-maze.com/identity-asp-net-core-project/
        //https://aspnetcore.readthedocs.io/en/stable/security/authentication/identity.html
        //https://wakeupandcode.com/authentication-authorization-in-asp-net-core-3-1/
    }
}
