using System;
using System.Reflection;
using Application.Shared.Decorators;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Decorators;
using Infrastructure.Domain.Users.Persistence.Configuration;
using Infrastructure.Domain.Users.Persistence.Repositories;
using Infrastructure.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ApplicationException = Application.Shared.Exceptions.ApplicationException;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton(configuration.GetSection(nameof(UsersDatabaseConfiguration))
                .Get<UsersDatabaseConfiguration>());
            services.AddValidatorsFromAssembly(typeof(ApplicationException).Assembly);
            services.AddMediatR(typeof(DependencyInjection), typeof(ApplicationException));
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(sp.GetService<UsersDatabaseConfiguration>().ConnectionString));
            services.AddTransient(sp =>
            {
                var options = sp.GetService<UsersDatabaseConfiguration>();
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Name);
            });
            services.AddTransient<IRedBetUserRepository, RedBetUserRepository>();
            services.AddTransient<IMrGreenUserRepository, MrGreenRepository>();
            services.Decorate(typeof(IRequestHandler<,>), typeof(ValidationCommandQueryHandlerDecorator<,>));
            services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingCommandQueryHandlerDecorator<,>));
            services.AddTransient<ErrorHandlerMiddleware>();
            return services;
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}