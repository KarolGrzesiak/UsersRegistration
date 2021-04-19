using System;
using MediatR;

namespace Application.Users.Commands.DeleteMrGreenUser
{
    public class DeleteMrGreenUser : IRequest
    {
        public Guid Id { get; set; }
    }
}