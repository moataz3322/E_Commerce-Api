using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.NewIdentityTest.Data
{
    //de 34an m8yr f al user
   internal class IdentetyTestDbContext : IdentityDbContext<ApplicationUserTest>
    {

        public IdentetyTestDbContext(DbContextOptions<IdentetyTestDbContext> options) : base(options)
        {

        }


        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<ApplicationUserTest>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<AdressTest>().ToTable("Adressess");


        }
    }
}
