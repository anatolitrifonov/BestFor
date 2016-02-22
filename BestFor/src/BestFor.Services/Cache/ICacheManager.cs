using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Cache
{
    public interface ICacheManager
    {
        object Get(string key);

        object Add(string key, object value);

        object Add(string key, object value, int seconds);
    }
}
