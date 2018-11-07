using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{

    /*Heredando de IdentityDbContext
     * para utilizar la autenticacion de Identity
     */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Hability> Habilities { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Proyect> Proyects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Proposal> Proposals { get; set; }



    }
}
