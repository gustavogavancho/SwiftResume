using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public interface IResumeRepository : IRepository<Resume>
{
    Task<IEnumerable<Resume>> GetResumesByUsername(string username);
    IEnumerable<Resume> GetTopResumes(int count);
    IEnumerable<Resume> GetResumes(int pageIndex, int pageSize);
}
