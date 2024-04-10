using AutoMapper;
using DAL.Contracts.App.Repositories;
using DAL.DTO;
using DAL.DTO.Search;
using DAL.EF.App.Mappers;
using DAL.EF.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class OrganizationRepository(AppDbContext dbContext, IMapper mapper) :
    BaseRepository<Organization, Domain.App.Organization, Guid, AppDbContext, OrganizationSearch>(
        dbContext, new OrganizationMapper(mapper)),
    IOrganizationRepository
{
    protected override IQueryable<Domain.App.Organization> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(o => o.PaymentMethod);
    }

    public override async Task<IEnumerable<Organization>> FindAllAsync(
        OrganizationSearch? search = default,
        bool noTracking = true)
    {
        search ??= new OrganizationSearch();

        var query = CreateQuery(noTracking);

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(e => e.Name.Contains(search.Name));

        if (!string.IsNullOrWhiteSpace(search.RegistrationNumber))
            query = query.Where(e => e.RegistrationNumber.Contains(search.RegistrationNumber));

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