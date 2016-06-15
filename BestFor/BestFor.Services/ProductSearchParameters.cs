using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;

namespace BestFor.Services
{
    /// <summary>
    /// Helper class to pass around as a search criteria for product search.
    /// </summary>
    public class ProductSearchParameters : IFirstIndex
    {
        public string Keyword { get; set; }

        /// <summary>
        /// For now let's say that we want to find one product per keyword.
        /// This property will be user for caching.
        /// It may become more complex later but for now one to one.
        /// </summary>
        public string IndexKey
        {
            get
            {
                // Why would someone 
                if (string.IsNullOrEmpty(Keyword) || string.IsNullOrWhiteSpace(Keyword))
                    throw new Exception("ProductSearchParameters IndexKey property is called with empty Keyword.");
                return Keyword;
            }
        }
    }
}
