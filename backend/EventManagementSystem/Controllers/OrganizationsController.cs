using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using EventManagementSystem.Mappers;
using Microsoft.AspNetCore.Mvc;
using PublicApi.v1.DTO;
using PublicApi.v1.DTO.Search;

namespace EventManagementSystem.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrganizationsController(IAppBll bll, IMapper mapper, ILogger<OrganizationsController> logger) : ControllerBase
    {
        private readonly IAppBll _bll = bll;
        private readonly IMapper _mapper = mapper;
        private readonly OrganizationMapper _eventMapper = new(mapper);
        private readonly ILogger<OrganizationsController> _logger = logger;

        [HttpGet]
        public async Task<IEnumerable<Organization>> GetOrganizations([FromQuery] OrganizationSearch? search)
        {
            _logger.LogInformation("GetOrganizations");

            search ??= new OrganizationSearch();

            return (await _bll.Organizations.FindAllAsync(_mapper.Map<OrganizationSearch, DAL.DTO.Search.OrganizationSearch>(search)))
                .Select(_eventMapper.Map);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(Guid id)
        {
            _logger.LogInformation("GetOrganization");

            var organization = await _bll.Organizations.FirstOrDefaultAsync(id);

            if (organization == null) return NotFound();

            return _eventMapper.Map(organization);
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            _logger.LogInformation("PostOrganization");

            var addedOrganization = _bll.Organizations.Add(_eventMapper.Map(organization));

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrganization", new { id = addedOrganization.Id }, _eventMapper.Map(addedOrganization));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(Guid id, Organization organization)
        {
            _logger.LogInformation("PutOrganization");

            if (id != organization.Id) return BadRequest();

            _bll.Organizations.Update(_eventMapper.Map(organization));

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(Guid id)
        {
            _logger.LogInformation("DeleteOrganization");

            var organization = await _bll.Organizations.FirstOrDefaultAsync(id);

            if (organization == null) return NotFound();

            _bll.Organizations.Remove(organization);

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
