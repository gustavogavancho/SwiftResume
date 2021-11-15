using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class ResumeRepository : Repository<Resume>, IResumeRepository
{
    private readonly SwiftResumeDbContext _context;

    public ResumeRepository(SwiftResumeDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Resume> GetTopResumes(int count)
    {
        return _context.Resumes.OrderByDescending(c => c.Nombres).Take(count).ToList();
    }

    public IEnumerable<Resume> GetResumes(int pageIndex, int pageSize)
    {
        return _context.Resumes
            .OrderBy(c => c.Nombres)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<IEnumerable<Resume>> GetResumesByUsername(string username)
    {
        return await _context.Resumes.Where(x => x.Username == username).ToListAsync();

    }
}
