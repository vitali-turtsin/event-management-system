using DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class BaseUow<TDbContext>(TDbContext uowDbContext) : BaseUow
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext = uowDbContext;

    public override Task<int> SaveChangesAsync()
    {
        return UowDbContext.SaveChangesAsync();
    }
}