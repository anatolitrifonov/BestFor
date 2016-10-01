using System.Diagnostics.CodeAnalysis;
using System;
using Microsoft.Extensions.Logging;

namespace BestFor.UnitTests.Testables
{
    [ExcludeFromCodeCoverage]
    public class TestLoggerFactory: ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger<object>();
        }

        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
