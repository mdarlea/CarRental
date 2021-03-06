﻿// <auto-generated />
using System;
using CarRental.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRental.Identity.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("CarRental.Identity.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("State")
                        .HasMaxLength(30);

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("SuiteNumber")
                        .HasMaxLength(10);

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CarRental.Identity.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(250);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("CreditCardId");

                    b.Property<int>("DriverLicenseId");

                    b.Property<string>("Email")
                        .HasMaxLength(250);

                    b.Property<short>("EmailConfirmed");

                    b.Property<short>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(250);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(250);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<short>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<short>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId")
                        .IsUnique();

                    b.HasIndex("DriverLicenseId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CarRental.Identity.Data.Entities.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BillingAddressId");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired();

                    b.Property<DateTime>("ExpirationTime");

                    b.Property<string>("NameOnCard")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId")
                        .IsUnique();

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("CarRental.Identity.Data.Entities.DriverLicense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryOfIssue")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("DriverLicenseNumber")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("StateOfIssue")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("DriverLicenses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(250);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
                        .HasMaxLength(250);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(250);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(250);

                    b.Property<string>("RoleId")
                        .HasMaxLength(250);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(250);

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarRental.Identity.Data.Entities.ApplicationUser", b =>
                {
                    b.HasOne("CarRental.Identity.Data.Entities.CreditCard", "CreditCard")
                        .WithOne()
                        .HasForeignKey("CarRental.Identity.Data.Entities.ApplicationUser", "CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarRental.Identity.Data.Entities.DriverLicense", "DriverLicense")
                        .WithOne()
                        .HasForeignKey("CarRental.Identity.Data.Entities.ApplicationUser", "DriverLicenseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("CarRental.Identity.Data.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<string>("ApplicationUserId");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(150);

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(150);

                            b1.Property<string>("MiddleName")
                                .HasMaxLength(150);

                            b1.HasKey("ApplicationUserId");

                            b1.ToTable("AspNetUsers");

                            b1.HasOne("CarRental.Identity.Data.Entities.ApplicationUser")
                                .WithOne("Name")
                                .HasForeignKey("CarRental.Identity.Data.ValueObjects.Name", "ApplicationUserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("CarRental.Identity.Data.Entities.CreditCard", b =>
                {
                    b.HasOne("CarRental.Identity.Data.Entities.Address", "BillingAddress")
                        .WithOne()
                        .HasForeignKey("CarRental.Identity.Data.Entities.CreditCard", "BillingAddressId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("CarRental.Identity.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarRental.Identity.Data.Entities.ApplicationUser")
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

                    b.HasOne("CarRental.Identity.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarRental.Identity.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
