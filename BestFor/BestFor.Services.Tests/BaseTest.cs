﻿using Autofac;
using BestFor.Fakes;
using BestFor.Data;

namespace BestFor.Services.Tests
{
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
