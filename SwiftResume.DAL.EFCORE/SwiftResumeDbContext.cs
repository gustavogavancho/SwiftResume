using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;

namespace SwiftResume.DAL.EFCORE;

public class SwiftResumeDbContext : DbContext
{
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Perfil> Perfiles { get; set; }
    public DbSet<Habilidad> Habilidades { get; set; }
    public DbSet<Educacion> Educacion { get; set; }
    public DbSet<Experiencia> Experiencia { get; set; }
    public DbSet<Certificacion> Certificaciones { get; set; }
    public DbSet<Proyecto> Proyectos { get; set; }
    public DbSet<InfoAdicional> InfoAdicional { get; set; }

    public SwiftResumeDbContext(DbContextOptions<SwiftResumeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seed user data
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Email = "ggavancholeon@gmail.com",
            Username = "GGAVANCHO",
            DateJoined = DateTime.Now,
            PasswordHashed = "AQAAAAEAACcQAAAAEMcloCaeJ2BYcGk+0LLGptkVnAjHoVr9npkXmqqRvVB2LmDnu1CW/tI0iX1KeKzIYA=="
        });
    }
}

