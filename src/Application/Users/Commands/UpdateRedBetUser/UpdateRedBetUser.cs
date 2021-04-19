using System;
using MediatR;

namespace Application.Users.Commands.UpdateRedBetUser
{
    public class UpdateRedBetUser : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public uint BuildingNumber { get; set; }
        public string ZipCode { get; set; }
        public string FavouriteFootballTeam { get; set; }
    }
}