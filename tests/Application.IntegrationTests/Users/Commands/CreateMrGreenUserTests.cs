using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.CreateMrGreenUser;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace Application.IntegrationTests.Users.Commands
{
    using static Shared;

    public class CreateMrGreenUserTests 
    {
        [Test]
        public void ShouldRequireFirstName()
        {
            var command = new CreateMrGreenUser
            {
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("FirstName is required")));
        }
        
        
        [Test]
        public void ShouldRequireFirstNameNotExceeding200Characters()
        {
            var firstNameLongerThan200Characters = new string('a', 201);
            var command = new CreateMrGreenUser
            {
                FirstName = firstNameLongerThan200Characters,
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("FirstName must not exceed 200 characters")));
        }
        
        [Test]
        public void ShouldRequireLastNameNameNotExceeding200Characters()
        {
            var firstNameLongerThan200Characters = new string('a', 201);
            var command = new CreateMrGreenUser
            {
                LastName = firstNameLongerThan200Characters,
                FirstName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("LastName must not exceed 200 characters")));
        }
        
        
        [Test]
        public void ShouldRequireLastName()
        {
            var command = new CreateMrGreenUser
            {
                FirstName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("LastName is required")));
        }
        
        
        [Test]
        public void ShouldRequireStreet()
        {
            var command = new CreateMrGreenUser
            {
                FirstName = "Test",
                LastName = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("Street is required")));
        }
        
        
        [Test]
        public void ShouldRequireBuildingNumber()
        {
            var command = new CreateMrGreenUser
            {
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                PersonalNumber = "11111-111",
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("BuildingNumber is required")));
        }
        
        [Test]
        public void ShouldRequirePersonalNumber()
        {
            var command = new CreateMrGreenUser
            {
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                ZipCode = "32-091"
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("PersonalNumber is required")));
        }
        
        
        [Test]
        public void ShouldRequireZipCode()
        {
            var command = new CreateMrGreenUser
            {
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
            };
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("ZipCode is required")));
        }
        
        [Test]
        public async Task ShouldCallCreateMethodOnRepository()
        {
            var command = new CreateMrGreenUser()
            {
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-111",
            };
            await SendAsync(command);
            MockMrGreenRepository.Verify(repository => repository.AddAsync(It.IsAny<MrGreenUser>()), Times.Once);
            
        }
        
        

        
    }
}