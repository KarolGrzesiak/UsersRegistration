using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRedBetUserRepository
    {
        Task AddAsync(RedBetUser user);
        Task UpdateAsync(RedBetUser user);
        Task<RedBetUser> GetAsync(Guid id);
        Task DeleteAsync(Guid id);

    }
}