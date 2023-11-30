using Autofac;
using Cinema.Domain.Data.Seed;
using Cinema.Domain.Services;

namespace Cinema.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CinemaContextDataSeeder>().AsSelf();
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();
        }
    }
}
