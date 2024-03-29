﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaijaStartupApp.Data;

namespace NaijaStartupApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191103114715_addTotalAmountToCompany")]
    partial class addTotalAmountToCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+AddOnService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<decimal>("Price");

                    b.Property<Guid?>("RegistrationId");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("AddOnService");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+ChatHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<Guid?>("CompanyId");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Group");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Subject");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatHeader");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+ChatThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<int?>("ChatId");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatThread");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Comp_Incentives", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("IncentiveId");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid?>("RegistrationId");

                    b.HasKey("Id");

                    b.HasIndex("IncentiveId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Comp_Incentives");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Company_Officers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<byte[]>("AddressFile");

                    b.Property<string>("Birth_Country");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Designation");

                    b.Property<string>("Dob");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Gender");

                    b.Property<string>("Id_Number");

                    b.Property<string>("Id_Type");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Nationality");

                    b.Property<byte[]>("PassportFile");

                    b.Property<string>("Phone_No");

                    b.Property<string>("PostalCode");

                    b.Property<Guid?>("RegistrationId");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Company_Officers");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Company_Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("AlternateCompanyName");

                    b.Property<string>("AlternateCompanyType");

                    b.Property<string>("ApprovalStatus");

                    b.Property<string>("BusinessActivity");

                    b.Property<string>("CacRegistrationNumber");

                    b.Property<string>("CompanyCapitalCurrency");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanyType");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("FinancialYearEnd");

                    b.Property<bool>("IsAddressRegistered");

                    b.Property<bool>("IsCacAvailable");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("LocalDirector");

                    b.Property<bool>("LocalResidentDirector");

                    b.Property<decimal>("LocalResidentDirectorPrice");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("NoOfSharesIssue");

                    b.Property<int?>("PackageId");

                    b.Property<string>("Postcode");

                    b.Property<bool>("RegCompleted");

                    b.Property<string>("ShareHolderName");

                    b.Property<decimal>("SharePrice");

                    b.Property<decimal>("SharesAllocated");

                    b.Property<string>("SndBusinessActivity");

                    b.Property<decimal>("TotalAmount");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("UserId");

                    b.ToTable("Company_Registration");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Message");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Incentives", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Description");

                    b.Property<string>("IncentiveName");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Incentives");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("PackageName");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Payments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("ApiRequest");

                    b.Property<string>("ApiResponse");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("CreatorUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DeletionTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("DeletionUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<decimal>("Discount");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Message");

                    b.Property<DateTime>("ModificationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("ModificationUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("PaymentType");

                    b.Property<Guid?>("RegistrationId");

                    b.Property<bool>("Status");

                    b.Property<decimal>("Tax");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<DateTime>("DeletionTime");

                    b.Property<string>("DeletionUserId");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModificationTime");

                    b.Property<string>("ModificationUserId");

                    b.Property<string>("Role");

                    b.Property<string>("State");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+AddOnService", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Company_Registration", "Registration")
                        .WithMany("addOnServices")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+ChatHeader", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Company_Registration", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("NaijaStartupApp.Models.NsuDtos+User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+ChatThread", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+ChatHeader", "Chat")
                        .WithMany("ChatThread")
                        .HasForeignKey("ChatId");

                    b.HasOne("NaijaStartupApp.Models.NsuDtos+User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Comp_Incentives", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Incentives", "Incentive")
                        .WithMany()
                        .HasForeignKey("IncentiveId");

                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Company_Registration", "Registration")
                        .WithMany("Comp_Incentives")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Company_Officers", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Company_Registration", "Registration")
                        .WithMany("company_Officers")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Company_Registration", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");

                    b.HasOne("NaijaStartupApp.Models.NsuDtos+User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NaijaStartupApp.Models.NsuDtos+Payments", b =>
                {
                    b.HasOne("NaijaStartupApp.Models.NsuDtos+Company_Registration", "Registration")
                        .WithMany("Payments")
                        .HasForeignKey("RegistrationId");
                });
#pragma warning restore 612, 618
        }
    }
}
