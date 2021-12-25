﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwiftResume.DAL.EFCORE;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    [DbContext(typeof(SwiftResumeDbContext))]
    partial class SwiftResumeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("SwiftResume.COMMON.Models.Habilidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Habilidades");
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Blog")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email2")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Github")
                        .HasColumnType("TEXT");

                    b.Property<string>("Linkedin")
                        .HasColumnType("TEXT");

                    b.Property<string>("Profesion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Resumen")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StackOverflow")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId")
                        .IsUnique();

                    b.ToTable("Perfiles");
                });

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
                            DateJoined = new DateTime(2021, 12, 20, 22, 49, 33, 214, DateTimeKind.Local).AddTicks(5002),
                            Email = "ggavancholeon@gmail.com",
                            PasswordHashed = "AQAAAAEAACcQAAAAEMcloCaeJ2BYcGk+0LLGptkVnAjHoVr9npkXmqqRvVB2LmDnu1CW/tI0iX1KeKzIYA==",
                            Username = "GGAVANCHO"
                        });
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Habilidad", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Habilidades")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Perfil", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithOne("Perfil")
                        .HasForeignKey("SwiftResume.COMMON.Models.Perfil", "ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Resume", b =>
                {
                    b.Navigation("Habilidades");

                    b.Navigation("Perfil");
                });
#pragma warning restore 612, 618
        }
    }
}
