using Application.Users.Queries.GetAllUsers;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Domain.Users.Persistence.Models
{
    public static class Extensions
    {
        public static User AsDatabaseModel(this MrGreenUser user) =>
            new()
            {
                Id = user.Id,
                FirstName = user.BasicUserInformation.FirstName,
                LastName = user.BasicUserInformation.LastName,
                Street = user.BasicUserInformation.Address.Street,
                BuildingNumber = user.BasicUserInformation.Address.Number,
                ZipCode = user.BasicUserInformation.Address.ZipCode,
                PersonalNumber = user.PersonalNumber.Value
            };
        
        public static User AsDatabaseModel(this RedBetUser user) =>
            new()
            {
                Id = user.Id,
                FirstName = user.BasicUserInformation.FirstName,
                LastName = user.BasicUserInformation.LastName,
                Street = user.BasicUserInformation.Address.Street,
                BuildingNumber = user.BasicUserInformation.Address.Number,
                ZipCode = user.BasicUserInformation.Address.ZipCode,
                FavouriteFootballTeam = user.FavouriteFootballTeam
            };

        public static MrGreenUser AsMrGreenUser(this User user) =>
            new(user.Id,
                new BasicUserInformation(user.FirstName, user.LastName,
                    new Address(user.Street, user.BuildingNumber, user.ZipCode)),
                new PersonalNumber(user.PersonalNumber));
        
        public static RedBetUser AsRedBetUser(this User user) =>
            new(user.Id,
                new BasicUserInformation(user.FirstName, user.LastName,
                    new Address(user.Street, user.BuildingNumber, user.ZipCode)),
                user.FavouriteFootballTeam);
        
        public static UserDTO AsDTO(this User user) =>
            new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = $"Street {user.Street} {user.BuildingNumber}, {user.ZipCode}",
                PersonalNumber = user.PersonalNumber,
                FavouriteFootballTeam = user.FavouriteFootballTeam
            };
    }
}