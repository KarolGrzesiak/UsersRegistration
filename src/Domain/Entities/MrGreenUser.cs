using Domain.ValueObjects;

namespace Domain.Entities
{
    public class MrGreenUser : AggregateRoot
    {
        public BasicUserInformation BasicUserInformation { get; private set; }
        public PersonalNumber PersonalNumber { get; private set; }

        public MrGreenUser(AggregateId id,BasicUserInformation basicUserInformation, PersonalNumber personalNumber)
        {
            Id = id;
            BasicUserInformation = basicUserInformation;
            PersonalNumber = personalNumber;
        }


        public void ChangeAddress(Address address)
        {
            BasicUserInformation = BasicUserInformation.ChangeAddress(address);
        }
        
        public void Rename(string firstName, string lastName)
        {
            BasicUserInformation = BasicUserInformation.Rename(firstName,lastName);
        }

        public void ChangePersonalNumber(PersonalNumber personalNumber)
        {
            PersonalNumber = personalNumber;
        }
    }
}