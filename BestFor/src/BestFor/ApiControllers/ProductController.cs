﻿using System;
using BestFor.Dto.AffiliateProgram;
using Microsoft.AspNet.Mvc;
using BestFor.Services.Services;
using BestFor.Services;

namespace BestFor.Controllers
{
    /// <summary>
    /// Api controller called from ReactJS control to search affiliate program products.
    /// </summary>
    [Route("api/[controller]")]
    public class ProductController : BaseApiController
    {
        protected string QUERY_STRING_PARAMETER_KEYWORD = "keyword";

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Call product service to look for product.
        /// </summary>
        /// <returns>One product</returns>
        // GET: api/product
        [HttpGet]
        public AffiliateProductDto Get()
        {
            // Input is passed in the query string. Interface might change eventually.
            // For now lets just get a keyword from query string and use that.
            // Do check the keyword though because our control will pass it otherwise what's the point.
            // Do input validation first because it is less expensive
            if (!Request.Query.ContainsKey(QUERY_STRING_PARAMETER_KEYWORD)) return null;
            var keyword = Request.Query[QUERY_STRING_PARAMETER_KEYWORD][0];
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrWhiteSpace(keyword)) return null;

            // Check if caller gave us security header.
            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return null.
            // Null in this case because we will also check that in react.
            if (!ParseAntiForgeryHeader()) return null;

            // Form the parameters object. This will get smarter then just keyword eventually.
            var parameters = new ProductSearchParameters() { Keyword = keyword };
            // Call the service
            var product = _productService.FindProduct(parameters);

            // this may be null but we are not going to do anything here no pint really. Just let the client side deal with results.
            return product;
        }
    }
}
