using Autofac;
using Autofac.Features.ResolveAnything;
using BLLStandard.Services;
using BLLStandard.ServicesInterfaces;
using DALStandard.Repositories;
using DALStandard.RepositoryInterfaces;
using System;
using Autofac.Integration.Wcf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ConsoleAppHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            builder.RegisterType<AreaRepository>().As<IAreaRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();
            builder.RegisterType<LayoutRepository>().As<ILayoutRepository>();
            builder.RegisterType<EventSeatRepository>().As<IEventSeatRepository>();
            builder.RegisterType<EventAreaRepository>().As<IEventAreaRepository>();
            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<PurchaseRepository>().As<IPurchaseRepository>();
            builder.RegisterType<SeatRepository>().As<ISeatRepository>();

            builder.RegisterType<AreaService>().As<IAreaService>();
            builder.RegisterType<VenueService>().As<IVenueService>();
            builder.RegisterType<LayoutService>().As<ILayoutService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();
            builder.RegisterType<SeatService>().As<ISeatServicece>();

            builder.RegisterType<WcfServiceLibrary.Services.EventService>().As<WcfServiceLibrary.ServicesInterfaces.IEventService>();
            builder.RegisterType<WcfServiceLibrary.Services.PurchaseService>().As<WcfServiceLibrary.ServicesInterfaces.IPurchaseService>();
            builder.RegisterType<WcfServiceLibrary.Services.VenueService>().As<WcfServiceLibrary.ServicesInterfaces.IVenueService>();
            builder.RegisterType<WcfServiceLibrary.Services.AreaService>().As<WcfServiceLibrary.ServicesInterfaces.IAreaService>();
            builder.RegisterType<WcfServiceLibrary.Services.LayoutService>().As<WcfServiceLibrary.ServicesInterfaces.ILayoutService>();
            builder.RegisterType<WcfServiceLibrary.Services.SeatService>().As<WcfServiceLibrary.ServicesInterfaces.ISeatServicece>();
            builder.RegisterType<WcfServiceLibrary.Services.PublicService>().As<WcfServiceLibrary.ServicesInterfaces.IPublicService>();

            var container = builder.Build();

            var host = new ServiceHost(typeof(WcfServiceLibrary.Services.EventService));
            host.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.EventService), container);
            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host.Open();
            Console.WriteLine("Event service started");

            var host2 = new ServiceHost(typeof(WcfServiceLibrary.Services.PurchaseService));
            host2.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.PurchaseService), container);
            host2.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host2.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host2.Open();
            Console.WriteLine("Purchase service started");

            var host3 = new ServiceHost(typeof(WcfServiceLibrary.Services.VenueService));
            host3.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.VenueService), container);
            host3.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host3.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host3.Open();
            Console.WriteLine("Venue service started");

            var host4 = new ServiceHost(typeof(WcfServiceLibrary.Services.AreaService));
            host4.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.AreaService), container);
            host4.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host4.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host4.Open();
            Console.WriteLine("Area service started");

            var host5 = new ServiceHost(typeof(WcfServiceLibrary.Services.LayoutService));
            host5.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.LayoutService), container);
            host5.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host5.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host5.Open();
            Console.WriteLine("Layout service started");

            var host6 = new ServiceHost(typeof(WcfServiceLibrary.Services.SeatService));
            host6.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.SeatService), container);
            host6.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host6.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host6.Open();
            Console.WriteLine("Seat service started");

            var host7 = new ServiceHost(typeof(WcfServiceLibrary.Services.PublicService));
            host7.AddDependencyInjectionBehavior(typeof(WcfServiceLibrary.Services.PublicService), container);
            host7.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host7.Authorization.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy> { new AuthorizationPolicy() });
            host7.Open();
            Console.WriteLine("Public service started");

            Console.Read();
        }
    }
}
