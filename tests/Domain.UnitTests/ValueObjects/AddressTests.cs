using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using Shouldly;

namespace Domain.UnitTests.ValueObjects
{
    public class AddressTests
    {
        [TestCase("123123-22")]
        [TestCase("12312-te")]
        [TestCase("XD21-02")]
        [TestCase("")]
        [TestCase("32-09!")]
        public void ShouldThrowInvalidZipCodeExceptionGivenInvalidZipcode(string zipcode)
        {
            var street = "Test";
            uint buildingNumber = 6;

            FluentActions.Invoking(() => new Address(street, buildingNumber, zipcode))
                .Should().Throw<InvalidZipcodeException>();
        }

        [Test]
        public void ShouldThrowInvalidBuildingNumberExceptionGivenBuildingNumberEqualToZero()
        {
            var street = "Test";
            var zipcode = "32-091";
            uint buildingNumber = 0;
            FluentActions.Invoking(() => new Address(street,  buildingNumber, zipcode))
                .Should().Throw<InvalidBuildingNumberException>();
        }
        
        
        [Test]
        public void ShouldReturnCorrectToStringFormat()
        {
            var street = "Test";
            uint buildingNumber = 6;
            var zipcode = "32-091";

            var address = new Address(street, buildingNumber, zipcode);

            address.ToString().Should().Be($"{street} {buildingNumber}, {zipcode}");
        }
        
        
        [Test]
        public void ShouldBeTheSameGivenAddressesWithTheSameValues()
        {
            var street = "Test";
            uint buildingNumber = 6;
            var zipcode = "32-091";

            var address1 = new Address(street, buildingNumber, zipcode);
            var address2 = new Address(street, buildingNumber, zipcode);
            
            address1.ShouldBe(address2);
        }
        
    }
}