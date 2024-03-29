﻿// <auto-generated />
using System;
using Exercise1.DbScaffold.Models.VirbelaListing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    [DbContext(typeof(VirbelaListingContext))]
    partial class VirbelaListingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exercise1.DbScaffold.Models.VirbelaListing.Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<int>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("listing");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 3, 21, 9, 15, 22, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Description = "Description for Listing A. Details for Listing A is provided here.",
                            Price = 12.34m,
                            Title = "Listing A"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2021, 4, 1, 13, 46, 52, 0, DateTimeKind.Unspecified),
                            CreatorId = 1,
                            Description = "Description for Listing B. Details for Listing B is provided here.",
                            Price = 22.34m,
                            Title = "Listing B"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2021, 3, 11, 15, 42, 59, 0, DateTimeKind.Unspecified),
                            CreatorId = 2,
                            Description = "Description for Listing C. Details for Listing C is provided here.",
                            Price = 32.45m,
                            Title = "Listing C"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2021, 2, 8, 14, 21, 46, 0, DateTimeKind.Unspecified),
                            CreatorId = 2,
                            Description = "Description for Listing D. Details for Listing D is provided here.",
                            Price = 455.56m,
                            Title = "Listing D"
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2021, 2, 27, 18, 19, 19, 0, DateTimeKind.Unspecified),
                            CreatorId = 3,
                            Description = "Description for Listing E. Details for Listing E is provided here.",
                            Price = 556.99m,
                            Title = "Listing E"
                        },
                        new
                        {
                            Id = 6,
                            CreatedDate = new DateTime(2021, 4, 1, 19, 43, 58, 0, DateTimeKind.Unspecified),
                            CreatorId = 4,
                            Description = "Description for Listing F. Details for Listing F is provided here.",
                            Price = 489.99m,
                            Title = "Listing F"
                        },
                        new
                        {
                            Id = 7,
                            CreatedDate = new DateTime(2021, 3, 31, 8, 22, 38, 0, DateTimeKind.Unspecified),
                            CreatorId = 4,
                            Description = "Description for Listing G. Details for Listing G is provided here.",
                            Price = 124.99m,
                            Title = "Listing G"
                        });
                });

            modelBuilder.Entity("Exercise1.DbScaffold.Models.VirbelaListing.Listinguser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Firstname")
                        .HasColumnName("firstname")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Lastname")
                        .HasColumnName("lastname")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("RegionId")
                        .HasColumnName("region_id")
                        .HasColumnType("int");

                    b.Property<string>("Userid")
                        .IsRequired()
                        .HasColumnName("userid")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.HasIndex("Userid")
                        .IsUnique()
                        .HasName("IX_userid");

                    b.ToTable("listinguser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "jsmith@contoso.com",
                            Firstname = "John",
                            Lastname = "Smith",
                            Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                            RegionId = 1,
                            Userid = "jsmith"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jdoe@contoso.com",
                            Firstname = "Jane",
                            Lastname = "Doe",
                            Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                            RegionId = 3,
                            Userid = "jdoe"
                        },
                        new
                        {
                            Id = 3,
                            Email = "lmessi@contoso.com",
                            Firstname = "Lionel",
                            Lastname = "Messi",
                            Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                            RegionId = 4,
                            Userid = "lmessi"
                        },
                        new
                        {
                            Id = 4,
                            Email = "maradona@contoso.com",
                            Firstname = "Diego",
                            Lastname = "Maradona",
                            Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                            RegionId = 1,
                            Userid = "maradona"
                        });
                });

            modelBuilder.Entity("Exercise1.DbScaffold.Models.VirbelaListing.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_name");

                    b.ToTable("region");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "B"
                        },
                        new
                        {
                            Id = 3,
                            Name = "C"
                        },
                        new
                        {
                            Id = 4,
                            Name = "D"
                        });
                });

            modelBuilder.Entity("Exercise1.DbScaffold.Models.VirbelaListing.Listing", b =>
                {
                    b.HasOne("Exercise1.DbScaffold.Models.VirbelaListing.Listinguser", "Creator")
                        .WithMany("Listing")
                        .HasForeignKey("CreatorId")
                        .HasConstraintName("FK_listing_creator_user")
                        .IsRequired();
                });

            modelBuilder.Entity("Exercise1.DbScaffold.Models.VirbelaListing.Listinguser", b =>
                {
                    b.HasOne("Exercise1.DbScaffold.Models.VirbelaListing.Region", "Region")
                        .WithMany("Listinguser")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_user_region")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
