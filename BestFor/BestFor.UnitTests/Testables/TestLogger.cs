﻿using System.Diagnostics.CodeAnalysis;
using System;
using Microsoft.Extensions.Logging;

namespace BestFor.UnitTests.Testables
{
    [ExcludeFromCodeCoverage]
    public class TestLogger<T> : ILogger, ILogger<T>
    {
        private object _scope;
        //private readonly ITestSink _sink;
        private readonly string _name;
        private readonly bool _enabled;

        public TestLogger()
        {
        }

        //public TestLogger(string name, ITestSink sink, bool enabled)
        //{
        //    //_sink = sink;
        //    _name = name;
        //    _enabled = enabled;
        //}

        public string Name { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            _scope = state;

            //_sink.Begin(new BeginScopeContext()
            //{
            //    LoggerName = _name,
            //    Scope = state,
            //});

            return TestDisposable.Instance;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //_sink.Write(new WriteContext()
            //{
            //    LogLevel = logLevel,
            //    EventId = eventId,
            //    State = state,
            //    Exception = exception,
            //    Formatter = (s, e) => formatter((TState)s, e),
            //    LoggerName = _name,
            //    Scope = _scope
            //});
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _enabled;
        }

        private class TestDisposable : IDisposable
        {
            public static readonly TestDisposable Instance = new TestDisposable();

            public void Dispose()
            {
                // intentionally does nothing
            }
        }
    }
}
