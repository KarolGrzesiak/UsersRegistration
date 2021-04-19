using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using Shouldly;

namespace Domain.UnitTests.ValueObjects
{
    public class BasicUserInformationTests
    {
        [Test]
        public void ShouldBeTheSameGivenInformationssWithTheSameValues()
        {
            var firstName = "Test";
            var lastName = "Test2";
            var address = new Address("Test", 6, "32-091");

            var information1 = new BasicUserInformation(firstName, lastName, address);
            var information2 = new BasicUserInformation(firstName, lastName, address);
            
            information1.ShouldBe(information2);
        }
        
        
        [Test]
        public void ShouldChangeTheAddress()
        {
            var firstName = "Test";
            var lastName = "Test2";
            var address = new Address("Test", 6, "32-091");
            var information = new BasicUserInformation(firstName, lastName, address);
            
            information.FirstName.ShouldBe(firstName);
            information.LastName.ShouldBe(lastName);
            information.Address.ShouldBe(address);

            var newAddress = new Address("Test2", 7, "31-091");
            information = information.ChangeAddress(newAddress);
            
            information.FirstName.ShouldBe(firstName);
            information.LastName.ShouldBe(lastName);
            information.Address.ShouldBe(newAddress);
        }
        
        [Test]
        public void ShouldChangeTheFirstAndLastNames()
        {
            var firstName = "Test";
            var lastName = "Test2";
            var address = new Address("Test", 6, "32-091");
            var information = new BasicUserInformation(firstName, lastName, address);
            
            information.FirstName.ShouldBe(firstName);
            information.LastName.ShouldBe(lastName);
            information.Address.ShouldBe(address);

            var newFirstName = "NewTest";
            var newLastName = "NewTest2";
            information = information.Rename(newFirstName, newLastName);
            
            information.FirstName.ShouldBe(newFirstName);
            information.LastName.ShouldBe(newLastName);
            information.Address.ShouldBe(address);
        }
        
        [Test]
        public void ShouldCreateNewObjectWhenRenaming()
        {
            var firstName = "Test";
            var lastName = "Test2";
            var address = new Address("Test", 6, "32-091");
            var information = new BasicUserInformation(firstName, lastName, address);
            
            var newFirstName = "NewTest";
            var newLastName = "NewTest2";
            var newInformation = information.Rename(newFirstName, newLastName);
            
            newInformation.ShouldNotBeSameAs(information);
        }
        
        
        [Test]
        public void ShouldCreateNewObjectWhenChangingAddress()
        {
            var firstName = "Test";
            var lastName = "Test2";
            var address = new Address("Test", 6, "32-091");
            var information = new BasicUserInformation(firstName, lastName, address);
            
            var newAddress = new Address("Test2", 7, "31-091");
            var newInformation = information.ChangeAddress(newAddress);
            
            newInformation.ShouldNotBeSameAs(information);
        }
    }
}