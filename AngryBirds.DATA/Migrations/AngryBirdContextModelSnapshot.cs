﻿// <auto-generated />
using AngryBirds.DATA.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AngryBirds.DATA.Migrations
{
    [DbContext(typeof(AngryBirdContext))]
    partial class AngryBirdContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AngryBirds.CORE.Models.Map", b =>
                {
                    b.Property<Guid>("MapId");

                    b.Property<int>("MaxMoves");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("MapId");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("AngryBirds.CORE.Models.Player", b =>
                {
                    b.Property<Guid>("PlayerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PlayerId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("AngryBirds.CORE.Models.Round", b =>
                {
                    b.Property<Guid>("RoundId");

                    b.Property<Guid>("MapId");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Points");

                    b.HasKey("RoundId");

                    b.HasIndex("MapId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Round");
                });

            modelBuilder.Entity("AngryBirds.CORE.Models.Round", b =>
                {
                    b.HasOne("AngryBirds.CORE.Models.Map", "Map")
                        .WithMany("Rounds")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AngryBirds.CORE.Models.Player", "Player")
                        .WithMany("Rounds")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
