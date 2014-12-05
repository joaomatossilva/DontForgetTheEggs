using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Variance;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data
{
    public class IocDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EggsContext>().AsSelf().InstancePerLifetimeScope();

            #region Setup of Mediator
            // this is needed to allow the Mediator to resolve contravariant handlers (not enabled by default in Autofac)
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterAssemblyTypes(typeof(IocDataModule).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterType<Mediator>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // to allow ShortBus to resolve lifetime-scoped dependencies properly, 
            // we really can't use the default approach of setting the static (global) dependency resolver, 
            // since that resolves instances from the root scope passed into it, rather than 
            // the current lifetime scope at the time of resolution.  
            // Resolving from the root scope can cause resource leaks, or in the case of components with a 
            // specific scope affinity (AutofacWebRequest, for example) it would fail outright, 
            // since that scope doesn't exist at the root level.
            builder.RegisterType<ShortBus.Autofac.AutofacDependencyResolver>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }

        #region Pin EntityFramework SqlProvider
        //this is a hack to pin a type from EntityFramework.SqlServer Assembly so it will
        //be copied directly to the assemblies referencing this one
        internal static Type TypePin;
        static IocDataModule()
        {
            TypePin = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        #endregion
    }

}
