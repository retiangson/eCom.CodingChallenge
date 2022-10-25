using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCom.CodingChallenge.Domain.Model
{
    public partial class eComDbContext : DbContext
    {
        public eComDbContext()
        {
        }

        public eComDbContext(DbContextOptions<eComDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Name> Names { get; set; } = null!;
        public virtual DbSet<Phone> Phones { get; set; } = null!;
        public virtual DbSet<PhoneType> PhoneTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Addresses_Id")
                    .IsUnique();

                entity.HasIndex(e => e.NameId, "IX_Addresses_NameId")
                    .IsUnique();

                entity.HasIndex(e => e.NameId, "IX_NameId");

                entity.HasOne(d => d.Name)
                    .WithOne(p => p.Address)
                    .HasForeignKey<Address>(d => d.NameId);
            });

            modelBuilder.Entity<Name>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Names_Id")
                    .IsUnique();
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Phones_Id")
                    .IsUnique();

                entity.HasIndex(e => e.NameId, "IX_NameId1");

                entity.HasIndex(e => e.PhoneTypeId, "IX_Phones_PhoneTypeId");

                entity.HasIndex(e => e.NameId, "IX_TypeId");

                entity.HasOne(d => d.Name)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.NameId);

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.PhoneTypeId);
            });

            modelBuilder.Entity<PhoneType>().HasData(
                    new PhoneType { Id = 1, Type = "Home" },
                    new PhoneType { Id = 2, Type = "Work" },
                    new PhoneType { Id = 3, Type = "Mobile" }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
