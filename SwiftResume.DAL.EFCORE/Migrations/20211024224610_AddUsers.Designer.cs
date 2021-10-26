﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwiftResume.DAL.EFCORE;

namespace SwiftResume.DAL.EFCORE.Migrations
{
    [DbContext(typeof(SwiftResumeDbContext))]
    [Migration("20211024224610_AddUsers")]
    partial class AddUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("SwiftResume.COMMON.Models.Resume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("BLOB");

                    b.Property<string>("Genero")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lenguaje")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Resumes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Apellidos = "Gavancho León",
                            Genero = "Masculino",
                            Lenguaje = "Español",
                            Nombres = "Gustavo"
                        },
                        new
                        {
                            Id = 2,
                            Apellidos = "Gavancho León",
                            Genero = "Masculino",
                            Lenguaje = "Español",
                            Nombres = "Jordi"
                        },
                        new
                        {
                            Id = 3,
                            Apellidos = "Iñipe Cachay",
                            Genero = "Femenino",
                            Lenguaje = "English",
                            Nombres = "Milagros"
                        },
                        new
                        {
                            Id = 4,
                            Apellidos = "Gavancho León",
                            Genero = "Femenino",
                            Lenguaje = "English",
                            Nombres = "Olga Cristina del Rocio"
                        },
                        new
                        {
                            Id = 5,
                            Apellidos = "León García",
                            Genero = "Femenino",
                            Lenguaje = "English",
                            Nombres = "Olga del Rocio"
                        },
                        new
                        {
                            Id = 6,
                            Apellidos = "Cordiva Rios",
                            Genero = "Femenino",
                            Lenguaje = "English",
                            Nombres = "Toty Ernestina"
                        },
                        new
                        {
                            Id = 7,
                            Apellidos = "Gavancho Cordova",
                            Genero = "Femenino",
                            Lenguaje = "English",
                            Nombres = "Mia Isabella"
                        });
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
