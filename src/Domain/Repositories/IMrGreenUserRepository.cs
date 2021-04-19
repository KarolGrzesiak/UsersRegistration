using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IMrGreenUserRepository
    {
        Task AddAsync(MrGreenUser user);
        Task UpdateAsync(MrGreenUser user);
        Task<MrGreenUser> GetAsync(Guid id);
        
        Task DeleteAsync(Guid id);
    }
}