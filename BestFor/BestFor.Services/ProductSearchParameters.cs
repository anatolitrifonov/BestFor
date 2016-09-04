using System;
using BestFor.Domain.Interfaces;

namespace BestFor.Services
{
    /// <summary>
    /// Helper class to pass around as a search criteria for product search.
    /// </summary>
    public class ProductSearchParameters : IFirstIndex
    {
        public string Keyword { get; set; }
        public string Category { get; set; }

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

                if (!string.IsNullOrEmpty(Category) && !string.IsNullOrWhiteSpace(Category))
                    return Keyword + "_" + Category;

                return Keyword;
            }
        }
    }
}
