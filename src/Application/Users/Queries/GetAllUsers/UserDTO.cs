using System;

namespace Application.Users.Queries.GetAllUsers
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string FavouriteFootballTeam { get; set; }
        public string Address { get; set; }
    }
}