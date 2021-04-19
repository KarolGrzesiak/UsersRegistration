using MediatR;

namespace Application.Users.Commands.CreateRedBetUser
{
    public class CreateRedBetUser : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public uint BuildingNumber { get; set; }
        public string ZipCode { get; set; }
        public string FavouriteFootballTeam { get; set; }
    }
}