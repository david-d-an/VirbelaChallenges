﻿// <auto-generated />
using System;
using Exercise1.DbScaffold.Models.VirbelaListing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    [DbContext(typeof(VirbelaListingContext))]
    [Migration("20210401184653_UpdateDb_1")]
    partial class UpdateDb_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
