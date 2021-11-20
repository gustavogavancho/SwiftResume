﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwiftResume.DAL.EFCORE;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    [DbContext(typeof(SwiftResumeDbContext))]
    [Migration("20211120194819_PasswordHasedColumnsChanged")]
    partial class PasswordHasedColumnsChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("SwiftResume.COMMON.Models.Resume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("BLOB");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lenguaje")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateJoined = new DateTime(2021, 11, 20, 14, 48, 19, 464, DateTimeKind.Local).AddTicks(2647),
                            Email = "ggavancholeon@gmail.com",
                            PasswordHashed = "AQAAAAEAACcQAAAAEMcloCaeJ2BYcGk+0LLGptkVnAjHoVr9npkXmqqRvVB2LmDnu1CW/tI0iX1KeKzIYA==",
                            Username = "GGAVANCHO"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
