using System;
using Book_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Book_Store.DB_Models
{
    public partial class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryContext()
        {
        }
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TbBook> TbBooks { get; set; }
        public virtual DbSet<TbDepartment> TbDepartments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-OG9VJN1;Database=Library;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbBook>(entity =>
            {
                entity.HasKey(e => e.BokId);

                entity.Property(e => e.BokId).HasColumnName("bok_Id");

                entity.Property(e => e.BokAuther)
                    .HasMaxLength(100)
                    .HasColumnName("bok_Auther");

                entity.Property(e => e.BokFile)
                    .HasMaxLength(100)
                    .HasColumnName("bok_File");

                entity.Property(e => e.BokImage)
                    .HasMaxLength(100)
                    .HasColumnName("bok_Image");

                entity.Property(e => e.BokName)
                    .HasMaxLength(100)
                    .HasColumnName("bok_Name");

                entity.Property(e => e.BokRate).HasColumnName("bok_Rate");

                entity.Property(e => e.DepId).HasColumnName("dep_Id");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.TbBooks)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK_TbBooks_TbDepartments");
            });

            modelBuilder.Entity<TbDepartment>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK_TbDepartment");

                entity.Property(e => e.DepId).HasColumnName("dep_Id");

                entity.Property(e => e.DepName)
                    .HasMaxLength(100)
                    .HasColumnName("dep_Name");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Saing).HasMaxLength(100);

                entity.Property(e => e.Titel).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
