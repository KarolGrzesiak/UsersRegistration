using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Queries.GetAllUsers;
using Infrastructure.Domain.Users.Persistence.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace Infrastructure.Domain.Users.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<Application.Users.Queries.GetAllUsers.GetAllUsers, IEnumerable<UserDTO>>
    {
        private readonly IMemoryCache _cache;
        private IMongoCollection<User> _users;

        public GetAllUsersHandler(IMongoDatabase mongoDatabase, IMemoryCache cache)
        {
            _cache = cache;
            _users = mongoDatabase.GetCollection<User>(nameof(User));
        }
        public async Task<IEnumerable<UserDTO>> Handle(Application.Users.Queries.GetAllUsers.GetAllUsers request, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue<IEnumerable<UserDTO>>(nameof(Application.Users.Queries.GetAllUsers.GetAllUsers), out var result))
            {
                var users = await _users.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
                result = users.Select(u => u.AsDTO());
                _cache.Set(nameof(Application.Users.Queries.GetAllUsers.GetAllUsers), result, TimeSpan.FromSeconds(10));
            }
             return result;
        }
    }
}