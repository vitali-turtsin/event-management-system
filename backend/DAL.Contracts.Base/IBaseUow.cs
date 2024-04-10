namespace DAL.Contracts.Base;

public interface IBaseUow
{
    Task<int> SaveChangesAsync();

    TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        where TRepository : class;
}