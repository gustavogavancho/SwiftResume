using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;

namespace SwiftResume.DAL.EFCORE;

public class SwiftResumeDbContext : DbContext
{
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<User> Users { get; set; }

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
            PasswordHash = "AQAAAAEAACcQAAAAEMcloCaeJ2BYcGk+0LLGptkVnAjHoVr9npkXmqqRvVB2LmDnu1CW/tI0iX1KeKzIYA=="
        });
    }
}

