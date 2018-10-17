using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using BLLStandard.DTO;
using BLLStandard.Services;
using BLLStandard.ServicesInterfaces;
using DALStandard.Repositories;
using DALStandard.RepositoryInterfaces;
using Hangfire;
using Microsoft.AspNet.Identity;
using System.Reflection;
using System.Web.Mvc;
using WebUI.Infrastructure;

namespace WebUI.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CustomUserStore>().As<IUserStore<UserDTO, string>>();
            builder.RegisterType<UserManager<UserDTO, string>>().AsSelf();

            builder.RegisterType<AreaRepository>().As<IAreaRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();            
            builder.RegisterType<LayoutRepository>().As<ILayoutRepository>();           
            builder.RegisterType<EventSeatRepository>().As<IEventSeatRepository>();
            builder.RegisterType<EventAreaRepository>().As<IEventAreaRepository>();
            builder.RegisterType<SeatRepository>().As<ISeatRepository>();
            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<PurchaseRepository>().As<IPurchaseRepository>();            


            builder.RegisterType<AreaService>().As<IAreaService>();
            builder.RegisterType<VenueService>().As<IVenueService>();
            builder.RegisterType<LayoutService>().As<ILayoutService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<SeatService>().As<ISeatServicece>();
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
        }
    }
}