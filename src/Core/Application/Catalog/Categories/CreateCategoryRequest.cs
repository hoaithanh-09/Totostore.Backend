using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Categories;

public class CreateCategoryRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsShowed { get; set; }
}

public class CreateCategoryRequestValidator : CustomValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator(IReadRepository<CategoryDetailDto> repository,
        IStringLocalizer<CreateCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CategoryByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["category.alreadyexists"], name));
}

public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<CategoryDetailDto> _repository;

    public CreateCategoryRequestHandler(IRepository<CategoryDetailDto> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new CategoryDetailDto(request.Name, request.Slug, request.Level, request.Order, request.Description,
            request.ParentId, request.IsShowed);

        // Add Domain Events to be raised after the commit
        category.DomainEvents.Add(EntityCreatedEvent.WithEntity(category));

        await _repository.AddAsync(category, cancellationToken);

        return category.Id;
    }
}