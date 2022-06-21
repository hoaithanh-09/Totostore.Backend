namespace Totostore.Backend.Application.Common.Models;

public static class PaginationResponseExtensions
{
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        this IReadRepositoryBase<T> repository, ISpecification<T, TDestination> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto
    {
        var list = await repository.ListAsync(spec, cancellationToken);
        int count = await repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
    }


    public static async Task<PaginationResponse<T>> NewPaginatedListAsync<T>(
        this IReadRepositoryBase<T> repository, List<T> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : class
    {
        int count = spec.Count();
        return new PaginationResponse<T>(spec, count, pageNumber, pageSize);
    }
}

