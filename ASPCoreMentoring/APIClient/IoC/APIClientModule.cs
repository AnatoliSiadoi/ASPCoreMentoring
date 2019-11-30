using System;
using APIClient.Client.Interfaces;
using APIClient.Client.Resources;
using Ninject;
using Ninject.Modules;
using RestEase;

namespace APIClient.IoC
{
    public class APIClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<RestClient>().ToMethod(context =>
            {
                return new RestClient(new Uri("http://localhost:51885/api/"));
            })
            .InSingletonScope();

            Bind<ICategoryResource>().ToMethod(context =>
            {
                var client = context.Kernel.Get<RestClient>();
                var api = client.For<ICategoryAPI>();
                return new CategoryResource(api);
            }).InSingletonScope();

            Bind<IProductResource>().ToMethod(context =>
            {
                var client = context.Kernel.Get<RestClient>();
                var api = client.For<IProductAPI>();
                return new ProductResource(api);
            }).InSingletonScope();
        }
    }
}
