using System;
using MediatR;

namespace Application.Users.Commands.DeleteRedBetUser
{
    public class DeleteRedBetUser : IRequest
    {
        public Guid Id { get; set; }
    }
}