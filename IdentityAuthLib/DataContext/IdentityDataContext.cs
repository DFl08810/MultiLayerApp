using IdentityAuthLib.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityAuthLib.DataContext
{
    public class IdentityDataContext : IdentityDbContext<User>
    {

        //public IdentityDataContext(DbContextOptions options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = TrainerConceptIdentity; Trusted_Connection = True;");
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    base.OnModelCreating(builder);
        //}
    }
}
