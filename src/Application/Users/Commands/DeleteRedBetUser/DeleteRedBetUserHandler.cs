using System.Threading;
using System.Threading.Tasks;
using Application.Users.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.DeleteRedBetUser
{
    public class DeleteRedBetUserHandler : IRequestHandler<DeleteRedBetUser>
    {
        private readonly IRedBetUserRepository _userRepository;

        public DeleteRedBetUserHandler(IRedBetUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteRedBetUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user is null)
                throw new UserNotFoundException(request.Id.ToString());
            await _userRepository.DeleteAsync(request.Id);
            return default;
        }
    }
}