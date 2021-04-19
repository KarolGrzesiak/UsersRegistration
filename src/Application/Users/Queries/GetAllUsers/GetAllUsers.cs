using System.Collections.Generic;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsers : IRequest<IEnumerable<UserDTO>>
    {
        
    }
}