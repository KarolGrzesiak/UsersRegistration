using Application.Shared.Exceptions;

namespace Application.Users.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string id) : base($"User with id: {id} was not found.")
        {
        }
    }
}