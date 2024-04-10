using AutoMapper;
using DAL.Contracts.App.Repositories;
using DAL.DTO;
using DAL.DTO.Search;
using DAL.EF.App.Mappers;
using DAL.EF.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PaymentMethodRepository(AppDbContext dbContext, IMapper mapper) :
    BaseRepository<PaymentMethod, Domain.App.PaymentMethod, Guid, AppDbContext, PaymentMethodSearch>(
        dbContext, new PaymentMethodMapper(mapper)),
    IPaymentMethodRepository
{
    public override async Task<IEnumerable<PaymentMethod>> FindAllAsync(
        PaymentMethodSearch? search = default,
        bool noTracking = true)
    {
        search ??= new PaymentMethodSearch();

        var query = CreateQuery(noTracking);

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(e => e.Name.Contains(search.Name));

        if (search.Ids != null && search.Ids.Count != 0)
            query = query.Where(e => search.Ids.Contains(e.Id));

        query = query.OrderBy(e => search.SortField);

        if (!search.IsAscending)
            query = query.Reverse();

        if (search.IsPagingEnabled)
            query = query.Skip(search.PageNumber * search.PageSize).Take(search.PageSize);

        return (await query.ToListAsync()).Select(Mapper.Map);
    }
}