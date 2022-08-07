﻿// <auto-generated />
using Infrastructure.Persistence.BoardContextConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BoardContext))]
    [Migration("20220805132202_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoardSnakeAndLader", b =>
                {
                    b.Property<int>("BoardsId")
                        .HasColumnType("int");

                    b.Property<int>("SnakesAndLadersId")
                        .HasColumnType("int");

                    b.HasKey("BoardsId", "SnakesAndLadersId");

                    b.HasIndex("SnakesAndLadersId");

                    b.ToTable("BoardsSnakesAndLaders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BoardEntities.Board", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("TotalBoxes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("Domain.Entities.BoardEntities.SnakeAndLader", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("EndBox")
                        .HasColumnType("int");

                    b.Property<bool>("IsLadder")
                        .HasColumnType("bit");

                    b.Property<int>("StartBox")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SnakeAndLader");
                });

            modelBuilder.Entity("BoardSnakeAndLader", b =>
                {
                    b.HasOne("Domain.Entities.BoardEntities.Board", null)
                        .WithMany()
                        .HasForeignKey("BoardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.BoardEntities.SnakeAndLader", null)
                        .WithMany()
                        .HasForeignKey("SnakesAndLadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}