using Autofac;
using BestFor.Data;
using BestFor.Fakes;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class BaseTest
    {
        protected IContainer resolver;

        public BaseTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FakeDataContext>().As<IDataContext>();
            resolver = builder.Build();
        }
    }
}
