using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class Shared
    {
        private static IServiceScopeFactory _scopeFactory;
        private static IConfigurationRoot _configuration;
        public static Mock<IRedBetUserRepository> MockRedBetRepository;
        public static Mock<IMrGreenUserRepository> MockMrGreenRepository;
        
        [OneTimeSetUp]
        public void RunBeforeTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            var services = new ServiceCollection();
            var startup = new Startup(_configuration);
                
            services.AddSingleton(Mock.Of<IHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "API"));
            startup.ConfigureServices(services);
            
            var currentRedBetRepository = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IRedBetUserRepository));
            var currentMrGreenRepository = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IMrGreenUserRepository));
            services.Remove(currentRedBetRepository);   
            services.Remove(currentMrGreenRepository);
            services.AddLogging();
            MockRedBetRepository = new Mock<IRedBetUserRepository>();
            MockMrGreenRepository = new Mock<IMrGreenUserRepository>();
            services.AddTransient(p => MockRedBetRepository.Object);
            services.AddTransient(p => MockMrGreenRepository.Object);
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
    
        }
        
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static void SetMrGreenRepositoryGetResult(Guid id, MrGreenUser user)
        {
            MockMrGreenRepository = new Mock<IMrGreenUserRepository>();
            MockMrGreenRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(user);
        }
        
        public static void SetRedBetRepositoryGetResult(Guid id, RedBetUser user)
        {
            MockRedBetRepository = new Mock<IRedBetUserRepository>();
            MockRedBetRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(user);
        }
    }
}