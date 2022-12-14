using Mapster;
using Totostore.Backend.Application.Catalog.ProductPrices;
using Totostore.Backend.Application.Catalog.Products;
using Totostore.Backend.Domain.Catalog;

namespace Totostore.Backend.Infrastructure.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<Product, ProductViewModel>.NewConfig();
        TypeAdapterConfig<ProductPrice, CreateProductPriceRequest>.NewConfig();
        TypeAdapterConfig<CreateProductPriceRequest, ProductPrice>.NewConfig();

        // here we will define the type conversion / Custom-mapping
        // More details at https://github.com/MapsterMapper/Mapster/wiki/Custom-mapping

        // This one is actually not necessary as it's mapped by convention
        // TypeAdapterConfig<Product, ProductDto>.NewConfig().Map(dest => dest.BrandName, src => src.Brand.Name);
    }
}