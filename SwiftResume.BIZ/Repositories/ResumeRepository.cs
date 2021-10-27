using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;
using System.Collections.Generic;
using System.Linq;

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
    }
}
