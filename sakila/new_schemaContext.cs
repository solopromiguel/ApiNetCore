using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication21.sakila;

namespace WebApplication21.sakila
{
    public partial class new_schemaContext : IdentityDbContext<Users, Role, int , 
                                            IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,
                                            IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public new_schemaContext()
        {      
        }

        public new_schemaContext(DbContextOptions<new_schemaContext> options)   : base(options){  }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Caracteristica> Caracteristicas { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<Evaluacion> Evaluacions { get; set; }
        public virtual DbSet<EtapaIdentificacion> Etapas { get; set; }

        public virtual DbSet<Identificacion> Identificacions { get; set; }

        public virtual DbSet<Riesgo> Riesgos { get; set; }
        public virtual DbSet<ControlRiesgo> ControlRiesgos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=123456;database=new_schema");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Roles)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public DbSet<WebApplication21.sakila.Control> Control { get; set; }
    }
}
