namespace BLL.Contracts.Base;

public interface IBaseBll
{
    Task<int> SaveChangesAsync();

    TService GetService<TService>(Func<TService> serviceCreationMethod)
        where TService : class;
}