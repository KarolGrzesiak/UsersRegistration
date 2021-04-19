using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.DeleteRedBetUser;
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
    public class DeleteRedBetUserTests
    {
        [Test]
        public void ShouldRequireId()
        {
            var command = new DeleteRedBetUser();
            FluentActions.Invoking(() =>
                    SendAsync(command)).Should().Throw<ValidationException>()
                .Where(ex => ex.Errors.Any(e => e.ErrorMessage.Equals("Id is required")));
        }
        
        [Test]
        public void ShouldThrowUserNotFoundExceptionGivenNoUser()
        {
            var id = Guid.NewGuid();
            var command = new DeleteRedBetUser
            {
                Id = id
            };
            SetRedBetRepositoryGetResult(id, null);
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UserNotFoundException>();
        }

        [Test]
        public async Task ShouldCallDeleteMethodOnRepository()
        {
            var id = Guid.NewGuid();
            var command = new DeleteRedBetUser
            {
                Id = id
            };
            SetRedBetRepositoryGetResult(id, new RedBetUser(id, new BasicUserInformation("Test","Test", new Address("Street", 10, "32-091")), "Test"));
            await SendAsync(command);
            MockRedBetRepository.Verify(repository => repository.DeleteAsync(id), Times.Once);
            
        }

    }
}