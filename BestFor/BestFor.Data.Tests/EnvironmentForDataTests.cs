﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BestFor.Data.Tests
{
    public class EnvironmentForDataTests
    {
        [Fact]
        public void EnvironmentForDataContext_Anatoli()
        {
            // This test does not help much at all.
            // Environment veriables can not quite be set independently and used on different environment.
            Environment.SetEnvironmentVariable("ConnectionString", "Anatoli");

            //var vars = Environment.GetEnvironmentVariables();
            //foreach (System.Collections.DictionaryEntry t in vars)
            //{
            //    if (t.Key.ToString().ToLower().Contains("connection"))
            //    {
            //        string h = "dfgh";
            //    }
            //    if (t.Value.ToString().ToLower().Contains("flv"))
            //    {
            //        string h1 = "dfgh";
            //    }
            //}
            var variable = Environment.GetEnvironmentVariable("ConnectionString");
            Assert.NotNull(variable);
        }
    }
}
