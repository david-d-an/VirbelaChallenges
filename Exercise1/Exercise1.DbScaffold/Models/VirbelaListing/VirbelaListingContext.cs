﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Exercise1.Common.Security;

namespace Exercise1.DbScaffold.Models.VirbelaListing
{
    public partial class VirbelaListingContext : DbContext
    {
        public VirbelaListingContext()
        {
        }

        public VirbelaListingContext(DbContextOptions<VirbelaListingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listing { get; set; }
        public virtual DbSet<Listinguser> Listinguser { get; set; }
        public virtual DbSet<Region> Region { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Staging Setting
                // "Server=tcp:virbelalisting.database.windows.net,1433;
                // Initial Catalog=VirbelaListing;
                // Persist Security Info=False;
                // User ID=appuser;
                // Password={password};
                // MultipleActiveResultSets=False;
                // Encrypt=True;TrustServerCertificate=False;"
                // var encConnStrVirbelaListing = "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpyZwH81uC883tyfXS2caR6rk0fTN1u/+dg05+L7sfLuDe8becDugt35NR2ahQEXCdVmHOs4JRAwWqvkL0EcqVVmwP4g1zUdfvg4yhzOtXVLmrf+xJFG6CFlCRw91hgUTCk5A6a2uPYHpKKiW7U0/cTZ6i9vKFqFJMvXxzRKU2hu3aMJ1iZsWgF3AR1jSwEOHQg=";

                // Development Setting
                // "Server=tcp:virbelalisting.database.windows.net,1433;
                // Initial Catalog=VirbelaListingDev;
                // Persist Security Info=False;
                // User ID=appuser;
                // Password={password};
                // MultipleActiveResultSets=False;
                // Encrypt=True;TrustServerCertificate=False;"
                var encConnStrVirbelaListing = "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpxmNDCVRydZ5efJTTL2O9FkiOGrbqlILlQPt5/8DcwssjosrrVeyxXrgIHB7pIN48IPOLp29HxT67vWGovw4jt+QtegcVynARe8g9XbGU6dB57kDogQ5t33I5iovM52B1o8tzRuYekLE/std6JtXC7McwscfvTKSE+85Woq7ljaLP6k5pRx83QaMvCe6Y7ICdAc5oKTzODrVpEZ+ae3uhaR";

                var connStrVirbelaListing = AesCryptoUtil.Decrypt(encConnStrVirbelaListing);
                optionsBuilder.UseSqlServer(connStrVirbelaListing);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.ToTable("listing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Listing)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_listing_creator_user");
            });

            modelBuilder.Entity<Listinguser>(entity =>
            {
                entity.ToTable("listinguser");

                entity.HasIndex(e => e.Userid)
                    .HasName("IX_userid")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Listinguser)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_region");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public partial class VirbelaListingContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Region>().HasData(new Region {
                Id = 1,
                Name = "A"
            });
            modelBuilder.Entity<Region>().HasData(new Region {
                Id = 2,
                Name = "B"
            });
            modelBuilder.Entity<Region>().HasData(new Region {
                Id = 3,
                Name = "C"
            });
            modelBuilder.Entity<Region>().HasData(new Region {
                Id = 4,
                Name = "D"
            });

            modelBuilder.Entity<Listinguser>().HasData(new Listinguser {
                Id = 1,
                Userid = "jsmith",
                Email = "jsmith@contoso.com",
                Firstname = "John",
                Lastname = "Smith",
                Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                RegionId = 1
            });
            modelBuilder.Entity<Listinguser>().HasData(new Listinguser {
                Id = 2,
                Userid = "jdoe",
                Email = "jdoe@contoso.com",
                Firstname = "Jane",
                Lastname = "Doe",
                Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                RegionId = 3
            });
            modelBuilder.Entity<Listinguser>().HasData(new Listinguser {
                Id = 3,
                Userid = "lmessi",
                Email = "lmessi@contoso.com",
                Firstname = "Lionel",
                Lastname = "Messi",
                Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                RegionId = 4
            });
            modelBuilder.Entity<Listinguser>().HasData(new Listinguser {
                Id = 4,
                Userid = "maradona",
                Email = "maradona@contoso.com",
                Firstname = "Diego",
                Lastname = "Maradona",
                Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
                RegionId = 1
            });

            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 1,
                Title = "Listing A",
                Description = "Description for Listing A. Details for Listing A is provided here.",
                Price = (decimal)12.34,
                CreatorId = 1,
                CreatedDate = new DateTime(2021, 03, 21, 09, 15, 22)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 2,
                Title = "Listing B",
                Description = "Description for Listing B. Details for Listing B is provided here.",
                Price = (decimal)22.34,
                CreatorId = 1,
                CreatedDate = new DateTime(2021, 04, 01, 13, 46, 52)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 3,
                Title = "Listing C",
                Description = "Description for Listing C. Details for Listing C is provided here.",
                Price = (decimal)32.45,
                CreatorId = 2,
                CreatedDate = new DateTime(2021, 03, 11, 15, 42, 59)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 4,
                Title = "Listing D",
                Description = "Description for Listing D. Details for Listing D is provided here.",
                Price = (decimal)455.56,
                CreatorId = 2,
                CreatedDate = new DateTime(2021, 02, 08, 14, 21, 46)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 5,
                Title = "Listing E",
                Description = "Description for Listing E. Details for Listing E is provided here.",
                Price = (decimal)556.99,
                CreatorId = 3,
                CreatedDate = new DateTime(2021, 02, 27, 18, 19, 19)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 6,
                Title = "Listing F",
                Description = "Description for Listing F. Details for Listing F is provided here.",
                Price = (decimal)489.99,
                CreatorId = 4,
                CreatedDate = new DateTime(2021, 04, 01, 19, 43, 58)
            });
            modelBuilder.Entity<Listing>().HasData(new Listing {
                Id = 7,
                Title = "Listing G",
                Description = "Description for Listing G. Details for Listing G is provided here.",
                Price = (decimal)124.99,
                CreatorId = 4,
                CreatedDate = new DateTime(2021, 03, 31, 08, 22, 38)
            });
        }
    }
}
