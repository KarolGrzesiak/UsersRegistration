using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Domain.Users.Persistence.Configuration;
using Infrastructure.Domain.Users.Persistence.Models;
using MongoDB.Driver;

namespace Infrastructure.Domain.Users.Persistence.Repositories
{
    public class RedBetUserRepository : IRedBetUserRepository
    {
        private IMongoCollection<User> _users;

        public RedBetUserRepository(IMongoDatabase mongoDatabase)
        {
            _users = mongoDatabase.GetCollection<User>(nameof(User));
        }
        public async Task AddAsync(RedBetUser user)
        {
            await _users.InsertOneAsync(user.AsDatabaseModel());
        }

        public async Task UpdateAsync(RedBetUser user)
        {
            await _users.ReplaceOneAsync( u=>u.Id.Equals(user.Id), user.AsDatabaseModel()); 
        }

        public async Task<RedBetUser> GetAsync(Guid id)
        {
            var user = await _users.Find( u=>u.Id.Equals(id)).SingleOrDefaultAsync();
            return user.AsRedBetUser();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _users.DeleteOneAsync( u=>u.Id.Equals(id)); 
        }
    }
}