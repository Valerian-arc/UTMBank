using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Core.Services.UserServices;
using Domain.Data.Context;
using Domain.Repositories;
using Helpers.Profiles;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Controllers;

namespace Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Initialize Autofac container
            var containerBuilder = new ContainerBuilder();

            // Register types and services
            RegisterTypes(containerBuilder);

            // Build the container
            var container = containerBuilder.Build();

            // Set Autofac as the Dependency Resolver for MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            // Register DbContext
            builder.RegisterType<BankDbContext>()
                   .AsSelf()
                   .InstancePerRequest();

            // Register generic repository
            builder.RegisterGeneric(typeof(GenericRepository<>))
                   .As(typeof(IGenericRepository<>))
                   .InstancePerRequest();

            // Register UserService
            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerRequest();

            // Register AutoMapper
            builder.Register(c =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new UserProfile());
                });

                var mapper = config.CreateMapper();
                return mapper;
            }).As<IMapper>().InstancePerRequest();

            // Register UserController explicitly
            builder.RegisterType<UserController>().InstancePerRequest();
        }
    }
}
