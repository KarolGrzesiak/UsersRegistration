using Domain.Entities;
using Domain.ValueObjects;
using NUnit.Framework;
using Shouldly;

namespace Domain.UnitTests.Entities
{
    public class RedBetUserTests
    {
        [Test]
        public void ShouldNotBeTheSameGivenUsersWithTheSameValuesButDifferentIds()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);
            var footballTeam = "TestTeam";

            var user1 = new RedBetUser(new AggregateId(), basicUserInformation, footballTeam);
            var user2 = new RedBetUser(new AggregateId(), basicUserInformation, footballTeam);
            
            user1.ShouldNotBe(user2);
        }
        
        [Test]
        public void ShouldBeTheSameGivenUsersWithTheSameIds()
        {
            var address1 = new Address("Test1", 6, "32-091");
            var address2 = new Address("Test2", 6, "32-091");
            var basicUserInformation1 = new BasicUserInformation("Test1", "Test1", address1);
            var basicUserInformation2 = new BasicUserInformation("Test2", "Test2", address2);
            var footballTeam1 = "TestTeam1";
            var footballTeam2 = "TestTeam2";
            var id = new AggregateId();
            
            var user1 = new RedBetUser(id, basicUserInformation1, footballTeam1);
            var user2 = new RedBetUser(id, basicUserInformation2, footballTeam2);
            
            user1.ShouldBe(user2);
        }
        
        
        [Test]
        public void ShouldChangeTheFirstAndLastNames()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);

            var user = new RedBetUser(new AggregateId(), basicUserInformation, "TestTeam");
            user.BasicUserInformation.FirstName.ShouldBe(basicUserInformation.FirstName);
            user.BasicUserInformation.LastName.ShouldBe(basicUserInformation.LastName);
            
            var newFirstName = "NewTest";
            var newLastName = "NewTest2";
            user.Rename(newFirstName, newLastName);
            
            user.BasicUserInformation.FirstName.ShouldBe(newFirstName);
            user.BasicUserInformation.LastName.ShouldBe(newLastName);
        }
        
        
        [Test]
        public void ShouldChangeAddress()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);

            var user = new RedBetUser(new AggregateId(), basicUserInformation, "TestTeam");
            user.BasicUserInformation.Address.ShouldBe(address);
            
            var newAddress = new Address("NewTest", 50, "60-091");
            user.ChangeAddress(newAddress);
            
            user.BasicUserInformation.Address.ShouldBe(newAddress);
        }
        
        
        [Test]
        public void ShouldLikeAnotherTeam()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);
            var footballTeam = "TestTeam1";
            
            var user = new RedBetUser(new AggregateId(), basicUserInformation, footballTeam);
            user.FavouriteFootballTeam.ShouldBe(footballTeam);

            var newFootballTeam = "TestTeam2";
            user.LikeFootballTeam(newFootballTeam);
            
            user.FavouriteFootballTeam.ShouldBe(newFootballTeam);
        }
    }
}