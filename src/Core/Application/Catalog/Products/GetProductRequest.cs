using Totostore.Backend.Application.Catalog.CategoryProducts;

namespace Totostore.Backend.Application.Catalog.Products;

public class GetProductRequest : IRequest<ProductDetailsDto>
{
    public Guid Id { get; set; }

    public GetProductRequest(Guid id) => Id = id;
}

public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductDetailsDto>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer<GetProductRequestHandler> _localizer;
    private readonly IRepository<ProductPrice> _productPriceRepository;
    private readonly IRepository<CategoryProduct> _categoryProductRepository;
    private readonly IRepository<CategoryDetailDto> _categoryRepository;

    public GetProductRequestHandler(IRepository<Product> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductDetailsDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetBySpecAsync((ISpecification<Product, ProductDetailsDto>)new ProductByIdWithSupplierSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(string.Format(_localizer["product.notfound"], request.Id));
        var category = _categoryProductRepository
                    .ListAsync(new CategoryByProductSpec(product.Id))
                    .Result.OrderByDescending(x=>x.CreatedOn).Select(x=>x.Category);
        // OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.ProductId == product.Id);
        var price = _productPriceRepository.ListAsync().Result.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x=>x.ProductId==product.Id);
        var result = new ProductDetailsDto()
        {
            Id = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            Rate = product.Rate,
            Quantity = product.Quantity,
            Status = product.Status,
            SupplierId = product.SupplierId,
            Supplier = product.Supplier,
            //CategoryId = category.CategoryId,
            //Category = category.Category,
            PriceId = price.Id,
            ProductPrice = price,
        };
        if(price.CouponId!=null)
        {
            result.CouponId = price.CouponId.Value;
            result.Coupon = price.Coupon;
                
        }
        return result;

    }
       
}