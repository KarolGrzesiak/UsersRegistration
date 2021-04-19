using System.Threading;
using System.Threading.Tasks;
using Application.Users.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.DeleteMrGreenUser
{
    public class DeleteMrGreenUserHandler : IRequestHandler<DeleteMrGreenUser>
    {
        private readonly IMrGreenUserRepository _userRepository;

        public DeleteMrGreenUserHandler(IMrGreenUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteMrGreenUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user is null)
                throw new UserNotFoundException(request.Id.ToString());
            await _userRepository.DeleteAsync(request.Id);
            return default;
        }
    }
}