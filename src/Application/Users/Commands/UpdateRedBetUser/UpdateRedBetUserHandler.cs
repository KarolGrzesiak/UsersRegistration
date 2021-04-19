using System.Threading;
using System.Threading.Tasks;
using Application.Users.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.UpdateRedBetUser
{
    public class UpdateRedBetUserHandler : IRequestHandler<UpdateRedBetUser>
    {
        private readonly IRedBetUserRepository _userRepository;

        public UpdateRedBetUserHandler(IRedBetUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(UpdateRedBetUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user is null)
                throw new UserNotFoundException(request.Id.ToString());
            UpdateUserEntity(user, request);
            await _userRepository.UpdateAsync(user);
            return default;
        }

        private void UpdateUserEntity(RedBetUser user, UpdateRedBetUser request)
        {
            var newAddress = new Address(request.Street, request.BuildingNumber, request.ZipCode);
            user.Rename(request.FirstName, request.LastName);
            user.ChangeAddress(newAddress);
            user.LikeFootballTeam(request.FavouriteFootballTeam);
        }
    }
}