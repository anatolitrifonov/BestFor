using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services
{
    /// <summary>
    /// Helper class to pass around as a search criteria for product search.
    /// </summary>
    public class ProductSearchParameters
    {
        public string Keyword { get; set; }
    }
}
