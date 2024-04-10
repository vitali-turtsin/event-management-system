using AutoMapper;
using DAL.Contracts.App.Repositories;
using DAL.DTO;
using DAL.DTO.Search;
using DAL.EF.App.Mappers;
using DAL.EF.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class EventRepository(AppDbContext dbContext, IMapper mapper) :
    BaseRepository<Event, Domain.App.Event, Guid, AppDbContext, EventSearch>(
        dbContext, new EventMapper(mapper)),
    IEventRepository
{
    protected override IQueryable<Domain.App.Event> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(e => e.People)!.ThenInclude(p => p!.PaymentMethod)
            .Include(e => e.Organizations)!.ThenInclude(o => o!.PaymentMethod);
    }

    public override async Task<IEnumerable<Event>> FindAllAsync(
        EventSearch? search = default,
        bool noTracking = true)
    {
        search ??= new EventSearch();

        var query = CreateQuery(noTracking);

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(e => e.Name.Contains(search.Name));

        if (search.StartDate != null)
            query = query.Where(e => e.DateTime == search.StartDate);

        if (!string.IsNullOrWhiteSpace(search.Location))
            query = query.Where(e => e.Location.Contains(search.Location));

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