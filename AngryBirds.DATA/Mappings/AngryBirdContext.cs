using AngryBirds.DATA.Entities;
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

            modelBuilder.Entity<Player>()
                .HasKey(p => p.PlayerId);

            modelBuilder.Entity<Map>()
                .HasKey(m => m.MapId);

            modelBuilder.Entity<Round>()
                .HasKey(r => r.RoundId);
        }
    }
}
