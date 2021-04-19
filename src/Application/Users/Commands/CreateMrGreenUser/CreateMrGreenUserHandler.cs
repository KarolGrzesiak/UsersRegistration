using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.CreateMrGreenUser
{
    public class CreateMrGreenUserHandler : IRequestHandler<CreateMrGreenUser>
    {
        private readonly IMrGreenUserRepository _userRepository;

        public CreateMrGreenUserHandler(IMrGreenUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(CreateMrGreenUser request, CancellationToken cancellationToken)
        {
            var id = new AggregateId();
            var address = new Address(request.Street, request.BuildingNumber, request.ZipCode);
            var basicInformation = new BasicUserInformation(request.FirstName, request.LastName, address);
            var personalNumber = new PersonalNumber(request.PersonalNumber);
            var user = new MrGreenUser(id, basicInformation, personalNumber);
            await _userRepository.AddAsync(user);
            return default;
        }
    }
}