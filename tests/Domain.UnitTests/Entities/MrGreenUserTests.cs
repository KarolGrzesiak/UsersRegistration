using System;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using Shouldly;

namespace Domain.UnitTests.Entities
{
    public class MrGreenUserTests
    {
        [Test]
        public void ShouldNotBeTheSameGivenUsersWithTheSameValuesButDifferentIds()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);
            var personalNumber = new PersonalNumber("12345-123");

            var user1 = new MrGreenUser(new AggregateId(), basicUserInformation, personalNumber);
            var user2 = new MrGreenUser(new AggregateId(), basicUserInformation, personalNumber);
            
            user1.ShouldNotBe(user2);
        }
        
        [Test]
        public void ShouldBeTheSameGivenUsersWithTheSameIds()
        {
            var address1 = new Address("Test1", 6, "32-091");
            var address2 = new Address("Test2", 6, "32-091");
            var basicUserInformation1 = new BasicUserInformation("Test1", "Test1", address1);
            var basicUserInformation2 = new BasicUserInformation("Test2", "Test2", address2);
            var personalNumber1 = new PersonalNumber("11111-111");
            var personalNumber2 = new PersonalNumber("22222-222");
            var id = new AggregateId();
            
            var user1 = new MrGreenUser(id, basicUserInformation1, personalNumber1);
            var user2 = new MrGreenUser(id, basicUserInformation2, personalNumber2);
            
            user1.ShouldBe(user2);
        }
        
        
        [Test]
        public void ShouldChangeTheFirstAndLastNames()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);
            var personalNumber = new PersonalNumber("12345-123");

            var user = new MrGreenUser(new AggregateId(), basicUserInformation, personalNumber);
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
            var personalNumber = new PersonalNumber("12345-123");

            var user = new MrGreenUser(new AggregateId(), basicUserInformation, personalNumber);
            user.BasicUserInformation.Address.ShouldBe(address);
            
            var newAddress = new Address("NewTest", 50, "60-091");
            user.ChangeAddress(newAddress);
            
            user.BasicUserInformation.Address.ShouldBe(newAddress);
        }
        
        
        [Test]
        public void ShouldChangePersonalNumber()
        {
            var address = new Address("Test", 6, "32-091");
            var basicUserInformation = new BasicUserInformation("Test", "Test2", address);
            var personalNumber = new PersonalNumber("12345-123");

            var user = new MrGreenUser(new AggregateId(), basicUserInformation, personalNumber);
            user.PersonalNumber.ShouldBe(personalNumber);
            
            var newPersonalNumber = new PersonalNumber("11111-111");
            user.ChangePersonalNumber(newPersonalNumber);
            
            user.PersonalNumber.ShouldBe(newPersonalNumber);
        }
    }
}