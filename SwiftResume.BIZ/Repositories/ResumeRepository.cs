using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SwiftResume.BIZ.Repositories
{
    public class ResumeRepository : Repository<Resume>, IResumeRepository
    {
        public ResumeRepository(SwiftResumeDbContextFactory context) : base(context)
        {
        }

        public IEnumerable<Resume> GetTopResumes(int count)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Resumes.OrderByDescending(c => c.Nombres).Take(count).ToList();
            }
        }

        public IEnumerable<Resume> GetResumes(int pageIndex, int pageSize)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Resumes
                    .OrderBy(c => c.Nombres)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
            }
        }

        public async Task<IEnumerable<Resume>> GetResumesByUsername(string username)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Resumes.Where(x => x.Username == username).ToListAsync();
            }

        }
    }
}
