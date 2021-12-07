using Autofac;
using PuppyPlace.Data;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Api;

public static class ContainerConfig
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<PersonsService>().As<IPersonsService>();

        return builder.Build();
    }
}