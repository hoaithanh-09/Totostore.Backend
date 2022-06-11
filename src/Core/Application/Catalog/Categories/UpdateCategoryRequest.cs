using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Categories;

public class UpdateCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public Guid ParentId { get; set; }
    public bool IsShowed { get; set; }
}

public class UpdateCategoryRequestValidator : CustomValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator(IReadRepository<Category> repository,
        IStringLocalizer<UpdateCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CategoryByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["category.alreadyexists"], name));
}

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Guid>
{
    private readonly IStringLocalizer<UpdateCategoryRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;

    public UpdateCategoryRequestHandler(IRepositoryWithEvents<Category> repository,
        IStringLocalizer<UpdateCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(string.Format(_localizer["category.notfound"], request.Id));

        category.Update(request.Name, request.Slug, request.Level, request.Order, request.Description, request.ParentId,
            request.IsShowed);
        // Add Domain Events to be raised after the commit
        category.DomainEvents.Add(EntityUpdatedEvent.WithEntity(category));

        await _repository.UpdateAsync(category, cancellationToken);

        return request.Id;
    }
}