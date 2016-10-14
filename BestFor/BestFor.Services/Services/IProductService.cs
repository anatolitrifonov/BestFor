using BestFor.Dto.AffiliateProgram;

namespace BestFor.Services.Services
{
    public interface IProductService
    {
        AffiliateProductDto FindProduct(ProductSearchParameters parameters);
    }
}
