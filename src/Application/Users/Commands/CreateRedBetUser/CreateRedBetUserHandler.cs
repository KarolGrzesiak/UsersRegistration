using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.CreateRedBetUser
{
    public class CreateRedBetUserHandler : IRequestHandler<CreateRedBetUser>
    {
        private readonly IRedBetUserRepository _userRepository;

        public CreateRedBetUserHandler(IRedBetUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(CreateRedBetUser request, CancellationToken cancellationToken)
        {
            var id = new AggregateId();
            var address = new Address(request.Street, request.BuildingNumber, request.ZipCode);
            var basicInformation = new BasicUserInformation(request.FirstName, request.LastName, address);
            var user = new RedBetUser(id, basicInformation, request.FavouriteFootballTeam);
            await _userRepository.AddAsync(user);
            return default;
        }
    }
}