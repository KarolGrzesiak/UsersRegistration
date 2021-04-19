using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using Shouldly;

namespace Domain.UnitTests.ValueObjects
{
    public class PersonalNumberTests
    {
        [Test]
        public void ShouldBeTheSameGivenPersonalNumbersWithTheSameValues()
        {
            var number = "12345-123";

            var personalNumber1 = new PersonalNumber(number);
            var personalNumber2 = new PersonalNumber(number);
            
            personalNumber1.ShouldBe(personalNumber2);
        }
        
        
        [TestCase("x1234!23")]
        [TestCase("")]
        [TestCase("12345-12")]
        [TestCase("1234!-123")]
        [TestCase("asda!-aaa")]
        [TestCase("abcde-aaa")]
        [TestCase("abcde-a-aa")]
        [TestCase("1234-5-123")]
        public void ShouldThrowInvalidPersonalNumberExceptionGivenInvalidPersonalNumber(string number)
        {
            FluentActions.Invoking(() => new PersonalNumber(number))
                .Should().Throw<InvalidPersonalNumberException>();
        }
    }
}