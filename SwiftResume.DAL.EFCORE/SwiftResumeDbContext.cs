using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;
using System;

namespace SwiftResume.DAL.EFCORE
{
    public class SwiftResumeDbContext : DbContext
    {
        public DbSet<Resume> Resumes { get; set; }

        public SwiftResumeDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resume>().HasData(new Resume
            {
                Id = 1,
                Nombres = "Gustavo",
                Apellidos = "Gavancho León",
                Genero = "Masculino",
                Lenguaje = "Español"
            });

            modelBuilder.Entity<Resume>().HasData(new Resume
            {
                Id = 2,
                Nombres = "Jordi",
                Apellidos = "Gavancho León",
                Genero = "Masculino",
                Lenguaje = "Español"
            });

            modelBuilder.Entity<Resume>().HasData(new Resume
            {
                Id = 3,
                Nombres = "Milagros",
                Apellidos = "Iñipe Cachay",
                Genero = "Femenino",
                Lenguaje = "English"
            });

            modelBuilder.Entity<Resume>().HasData(new Resume
            {
                Id = 4,
                Nombres = "Olga Cristina del Rocio",
                Apellidos = "Gavancho León",
                Genero = "Femenino",
                Lenguaje = "English"
            });

            modelBuilder.Entity<Resume>().HasData(new Resume
            {
                Id = 5,
                Nombres = "Olga del Rocio",
                Apellidos = "León García",
                Genero = "Femenino",
                Lenguaje = "English"
            });
        }
    }
}
