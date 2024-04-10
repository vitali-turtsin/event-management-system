using AutoMapper;
using DAL.Contracts.App.Repositories;
using DAL.DTO;
using DAL.DTO.Search;
using DAL.EF.App.Mappers;
using DAL.EF.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PersonRepository(AppDbContext dbContext, IMapper mapper) :
    BaseRepository<Person, Domain.App.Person, Guid, AppDbContext, PersonSearch>(
        dbContext, new PersonMapper(mapper)),
    IPersonRepository
{
    protected override IQueryable<Domain.App.Person> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(o => o.PaymentMethod);
    }

    public override async Task<IEnumerable<Person>> FindAllAsync(
        PersonSearch? search = default,
        bool noTracking = true)
    {
        search ??= new PersonSearch();

        var query = CreateQuery(noTracking);

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(e => (e.FirstName + " " + e.LastName).Contains(search.Name)
            || (e.LastName + " " + e.FirstName).Contains(search.Name));

        if (!string.IsNullOrWhiteSpace(search.PersonalCode))
            query = query.Where(e => e.PersonalCode.Contains(search.PersonalCode));

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