using Domain.ValueObjects;

namespace Domain.Entities
{
    public class RedBetUser : AggregateRoot
    {
        public BasicUserInformation BasicUserInformation { get; private set; }
        public string FavouriteFootballTeam { get; private set; }

        public RedBetUser(AggregateId id,BasicUserInformation basicUserInformation, string favouriteFootballTeam)
        {
            Id = id;
            BasicUserInformation = basicUserInformation;
            FavouriteFootballTeam = favouriteFootballTeam;
        }
        
        public void ChangeAddress(Address address)
        {
            BasicUserInformation = BasicUserInformation.ChangeAddress(address);
        }
        
        public void Rename(string firstName, string lastName)
        {
            BasicUserInformation = BasicUserInformation.Rename(firstName,lastName);
        }

        public void LikeFootballTeam(string footballTeam)
        {
            FavouriteFootballTeam = footballTeam;
        }
    }
}