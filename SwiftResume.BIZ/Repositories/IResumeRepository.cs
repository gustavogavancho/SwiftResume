using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public interface IResumeRepository : IRepository<Resume>
{
    Task<Resume> GetResumeWithProfile(int id);
    Task<IEnumerable<Resume>> GetResumesByUsername(string username);
    Task<Resume> GetResumeWithProfileHabilidadesEducacionOtros(int id);
}
