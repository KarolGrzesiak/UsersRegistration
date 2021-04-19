using System.Threading;
using System.Threading.Tasks;
using Application.Users.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.UpdateMrGreenUser
{
    public class UpdateMrGreenUserHandler : IRequestHandler<UpdateMrGreenUser>
    {
        private readonly IMrGreenUserRepository _userRepository;

        public UpdateMrGreenUserHandler(IMrGreenUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(UpdateMrGreenUser request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetAsync(request.Id);
            if (user is null)
                throw new UserNotFoundException(request.Id.ToString());
            UpdateUserEntity(user, request);
            await _userRepository.UpdateAsync(user);
            return default;
        }

        private void UpdateUserEntity(MrGreenUser user, UpdateMrGreenUser request)
        {
            var newAddress = new Address(request.Street, request.BuildingNumber, request.ZipCode);
            var newPersonalNumber = new PersonalNumber(request.PersonalNumber);
            user.Rename(request.FirstName, request.LastName);
            user.ChangeAddress(newAddress);
            user.ChangePersonalNumber(newPersonalNumber);
        }
    }
}