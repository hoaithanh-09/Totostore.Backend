﻿using Totostore.Backend.Application.Catalog.CategoryProducts;

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

    public GetProductRequestHandler(IRepository<Product> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductDetailsDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);
        var model = await _repository.GetBySpecAsync((ISpecification<Product, ProductDetailsDto>)new ProductByIdWSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(string.Format(_localizer["product.notfound"], request.Id));
        return model;
    }
}