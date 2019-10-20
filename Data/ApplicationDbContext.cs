using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static NaijaStartupApp.Models.NsuDtos;

namespace NaijaStartupApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<Company_Registration> Company_Registration { get; set; }
        public virtual DbSet<Company_Officers> Company_Officers { get; set; }
        public virtual DbSet<AddOnService> AddOnService { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletionTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModificationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            });
            modelBuilder.Entity<Company_Registration>(entity =>
            {
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.RegCompleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ApprovalStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DeletionTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModificationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            });
            modelBuilder.Entity<Company_Officers>(entity =>
            {
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                
                entity.Property(e => e.DeletionTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModificationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            });
            modelBuilder.Entity<AddOnService>(entity =>
            {
                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");


                entity.Property(e => e.DeletionTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModificationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            });
            modelBuilder.Entity<Payments>(entity =>
            {
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.DeletionTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModificationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            });

        }

    }
}


