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

    public async Task<IEnumerable<Resume>> GetResumesByUsername(string username)
    {
        return await _context.Resumes.Where(x => x.Username == username).ToListAsync();
    }

    public async Task<Resume> GetResumeWithProfile(int id)
    {
        return await _context.Resumes.Include(x=> x.Perfil).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Resume> GetResumeWithProfileHabilidades(int id)
    {
        return await _context.Resumes.Include(x => x.Perfil)
            .Include(x => x.Habilidades).FirstOrDefaultAsync(x => x.Id == id);
    }
}
