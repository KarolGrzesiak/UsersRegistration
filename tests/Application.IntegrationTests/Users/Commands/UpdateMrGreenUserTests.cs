using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.UpdateMrGreenUser;
using Application.Users.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace Application.IntegrationTests.Users.Commands
{
    using static Shared;
    public class UpdateMrGreenUserTests
    {
        
        [Test]
        public void ShouldRequireAllFields()
        {
            var command = new UpdateMrGreenUser();
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.All(e => 
                    e.ErrorMessage.Equals("Id is required")
                    || e.ErrorMessage.Equals("FirstName is required")
                    || e.ErrorMessage.Equals("LastName is required")
                    || e.ErrorMessage.Equals("Street is required")
                    || e.ErrorMessage.Equals("BuildingNumber is required")
                    || e.ErrorMessage.Equals("ZipCode is required")
                    || e.ErrorMessage.Equals("PersonalNumber is required")
                    ));
        }
        
        [Test]
        public void ShouldThrowUserNotFoundExceptionGivenNoUser()
        {
            var id = Guid.NewGuid();
            var command = new UpdateMrGreenUser
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-111"
            };
            SetMrGreenRepositoryGetResult(id, null);
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UserNotFoundException>();
        }

        [Test]
        public async Task ShouldCallUpdateMethodOnRepository()
        {
            var id = Guid.NewGuid();
            var command = new UpdateMrGreenUser
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                Street = "Test",
                BuildingNumber = 10,
                PersonalNumber = "11111-111",
                ZipCode = "32-111"
            };
            SetMrGreenRepositoryGetResult(id, new MrGreenUser(id, new BasicUserInformation("Test","Test", new Address("Street", 10, "32-091")), new PersonalNumber("11111-111")));
            await SendAsync(command);
            MockMrGreenRepository.Verify(repository => repository.UpdateAsync(It.IsAny<MrGreenUser>()), Times.Once);
            
        }
    }
}