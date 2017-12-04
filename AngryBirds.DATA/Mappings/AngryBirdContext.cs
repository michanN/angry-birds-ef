using AngryBirds.CORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.DATA.Mappings
{
    public class AngryBirdContext : DbContext
    {
        public AngryBirdContext(DbContextOptions<AngryBirdContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Map>().ToTable("Map");
            modelBuilder.Entity<Round>().ToTable("Round");

            // players
            modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);
            modelBuilder.Entity<Player>().Property(p => p.PlayerId).ValueGeneratedNever();
            modelBuilder.Entity<Player>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            // maps
            modelBuilder.Entity<Map>().HasKey(m => m.MapId);
            modelBuilder.Entity<Map>().Property(m => m.MapId).ValueGeneratedNever();
            modelBuilder.Entity<Map>()
                .Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Map>()
                .Property(m => m.MaxMoves)
                .IsRequired();

            // rounds
            modelBuilder.Entity<Round>().HasKey(r => r.RoundId);
            modelBuilder.Entity<Round>().Property(r => r.RoundId).ValueGeneratedNever();
            modelBuilder.Entity<Round>().Property(r => r.Points).IsRequired();

            modelBuilder.Entity<Round>()
                .HasOne(r => r.Player)
                .WithMany(p => p.Rounds)
                .HasForeignKey(r => r.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Round>()
                .HasOne(r => r.Map)
                .WithMany(m => m.Rounds)
                .HasForeignKey(r => r.MapId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
