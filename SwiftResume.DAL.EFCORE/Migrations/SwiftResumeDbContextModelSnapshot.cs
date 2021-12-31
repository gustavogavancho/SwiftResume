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

            modelBuilder.Entity("SwiftResume.COMMON.Models.Certificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Horas")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Institucion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ponente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoActividad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoCertificado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Certificaciones");
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Educacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Institucion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Meritos")
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoEducacion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Educacion");
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Experiencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("EsActual")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Institucion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Logros")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lugar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Mostrar")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Responsabilidades")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Experiencia");
                });

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

            modelBuilder.Entity("SwiftResume.COMMON.Models.InfoAdicional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoInfoAdicional")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("InfoAdicional");
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

            modelBuilder.Entity("SwiftResume.COMMON.Models.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResumeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoProyecto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Proyectos");
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
                            DateJoined = new DateTime(2021, 12, 31, 16, 20, 28, 885, DateTimeKind.Local).AddTicks(3928),
                            Email = "ggavancholeon@gmail.com",
                            PasswordHashed = "AQAAAAEAACcQAAAAEMcloCaeJ2BYcGk+0LLGptkVnAjHoVr9npkXmqqRvVB2LmDnu1CW/tI0iX1KeKzIYA==",
                            Username = "GGAVANCHO"
                        });
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Certificacion", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Certificacion")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Educacion", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Educacion")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Experiencia", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Experiencia")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Habilidad", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Habilidades")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.InfoAdicional", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("InfoAdicional")
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

            modelBuilder.Entity("SwiftResume.COMMON.Models.Proyecto", b =>
                {
                    b.HasOne("SwiftResume.COMMON.Models.Resume", null)
                        .WithMany("Proyectos")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwiftResume.COMMON.Models.Resume", b =>
                {
                    b.Navigation("Certificacion");

                    b.Navigation("Educacion");

                    b.Navigation("Experiencia");

                    b.Navigation("Habilidades");

                    b.Navigation("InfoAdicional");

                    b.Navigation("Perfil");

                    b.Navigation("Proyectos");
                });
#pragma warning restore 612, 618
        }
    }
}
