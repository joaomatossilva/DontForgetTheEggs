using AutoMapper;
using AutoMapper.Configuration;
using DontForgetTheEggs.Core.Data;
using MediatR;
using StructureMap;
using StructureMap.Pipeline;

namespace DontForgetTheEggs.Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<CoreRegistry>();
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                scanner.AddAllTypesOf<Profile>();
            });

            For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);

            For<EggsDbContext>().Use("db context", () => new EggsDbContext("ApplicationDb")).LifecycleIs<UniquePerRequestLifecycle>();

            //Autommaper config
            Mapper.Initialize(cfg => {
                cfg.AddProfiles(typeof(CoreRegistry).Assembly);
            });
        }
    }
}
