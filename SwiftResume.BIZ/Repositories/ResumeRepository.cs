﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<Resume> GetResumeWithProfileHabilidadesEducacionOtros(int id)
    {
        return await _context.Resumes
            .Include(x => x.Perfil)
            .Include(x => x.Habilidades)
            .Include(x => x.Educacion)
            .Include(x=> x.Experiencia)
            .Include(x=> x.Proyectos)
            .Include(x=> x.Certificacion)
            .Include(x=> x.InfoAdicional)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
