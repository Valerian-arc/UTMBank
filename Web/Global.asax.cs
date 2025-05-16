using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Core.Profiles;
using Core.Services.CardServices;
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

            // Register CardService
            builder.RegisterType<CardService>()
                .As<ICardService>()
                .InstancePerRequest();

            // Register AutoMapper
            builder.Register(c =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new UserProfile());
                    cfg.AddProfile(new CardProfile());
                });

                var mapper = config.CreateMapper();
                return mapper;
            }).As<IMapper>().InstancePerRequest();

            // Register UserController explicitly
            builder.RegisterType<UserController>().InstancePerRequest();
            builder.RegisterType<CardController>().InstancePerRequest();
            builder.RegisterType<AdminController>().InstancePerRequest();
        }
    }
}
