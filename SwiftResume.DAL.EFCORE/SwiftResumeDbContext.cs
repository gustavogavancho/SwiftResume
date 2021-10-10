using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;

namespace SwiftResume.DAL.EFCORE
{
    public class SwiftResumeDbContext : DbContext
    {
        public DbSet<Resume> Resumes { get; set; }

        public SwiftResumeDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
