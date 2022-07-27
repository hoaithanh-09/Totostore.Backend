using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totostore.Backend.Application.Catalog.Products;
public interface IProductService : ITransientService
{
    public Task<PaginationResponse<ProductDetailsDto>> SearchAsync(SearchProductsRequest request, CancellationToken cancellationToken);
    public Task<ProductDetailsDto> GetById(Guid id);
}
