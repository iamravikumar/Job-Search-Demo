using jobApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace jobApp.Context
{
    public class JobContext : IdentityDbContext
    {
        public JobContext() : base("JobContext") { }
        public DbSet<Job> Jobs1 { get; set; }

        public DbSet<Submit> Submit { get; set; }


        //public System.Data.Entity.DbSet<jobApp.Models.Apply> Careers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
           .HasRequired(n => n.ApplicationUser)
           .WithMany(a => a.job)
           .HasForeignKey(n => n.ApplicationUserId)
           .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Apply>()
            //.HasRequired(n => n.Job)
            //.WithMany(a => a.Applies)
            //.HasForeignKey(n => n.jobid)
            //.WillCascadeOnDelete(false);


        }
    }
}