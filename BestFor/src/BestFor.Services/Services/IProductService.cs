using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BestFor.Dto.AffiliateProgram;

namespace BestFor.Services.Services
{
    public interface IProductService
    {
        AffiliateProductDto FindProduct(ProductSearchParameters parameters);
    }
}
