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
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Incentives> Incentives { get; set; }
        public virtual DbSet<Comp_Incentives> Comp_Incentives { get; set; }
        public virtual DbSet<ChatHeader> ChatHeader { get; set; }
        public virtual DbSet<ChatThread> ChatThread { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Package>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Contact>(entity =>
            {
                    entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");



            });
            modelBuilder.Entity<Company_Registration>(entity =>
            {
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Company_Officers>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<AddOnService>(entity =>
            {
                entity.Property(e => e.ServiceName)
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
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Payments>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Package>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Incentives>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Comp_Incentives>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<ChatHeader>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<ChatThread>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            modelBuilder.Entity<Settings>(entity =>
            {

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletionTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.ModificationTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModificationUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.CreatorUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.DeletionUserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            });
            //modelBuilder.Entity<User>().HasData(
            //new User() { UserName = "admin", Email="NaijaStarup@gmail.com", Role = "Admin" });

        }

    }
}


