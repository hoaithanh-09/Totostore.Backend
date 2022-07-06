namespace Totostore.Backend.Application.Catalog.Categories;

public class GetCategoryRequest : IRequest<CategoryDto>
{
    public GetCategoryRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class CategoryByIdSpec : Specification<CategoryDetailDto, CategoryDto>, ISingleResultSpecification
{
    public CategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, CategoryDto>
{
    private readonly IStringLocalizer<GetCategoryRequestHandler> _localizer;
    private readonly IRepository<CategoryDetailDto> _repository;

    public GetCategoryRequestHandler(IRepository<CategoryDetailDto> repository,
        IStringLocalizer<GetCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CategoryDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<CategoryDetailDto, CategoryDto>)new CategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["category.notfound"], request.Id));
}