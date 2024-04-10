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
    public class PeopleController(IAppBll bll, IMapper mapper, ILogger<PeopleController> logger) : ControllerBase
    {
        private readonly IAppBll _bll = bll;
        private readonly IMapper _mapper = mapper;
        private readonly PersonMapper _eventMapper = new(mapper);
        private readonly ILogger<PeopleController> _logger = logger;

        [HttpGet]
        public async Task<IEnumerable<Person>> GetPeople([FromQuery] PersonSearch? search)
        {
            _logger.LogInformation("GetPeople");

            search ??= new PersonSearch();

            return (await _bll.People.FindAllAsync(_mapper.Map<PersonSearch, DAL.DTO.Search.PersonSearch>(search)))
                .Select(_eventMapper.Map);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            _logger.LogInformation("GetPerson");

            var person = await _bll.People.FirstOrDefaultAsync(id);

            if (person == null) return NotFound();

            return _eventMapper.Map(person);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _logger.LogInformation("PostPerson");

            var addedPerson = _bll.People.Add(_eventMapper.Map(person));

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = addedPerson.Id }, _eventMapper.Map(addedPerson));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            _logger.LogInformation("PutPerson");

            if (id != person.Id) return BadRequest();

            _bll.People.Update(_eventMapper.Map(person));

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            _logger.LogInformation("DeletePerson");

            var person = await _bll.People.FirstOrDefaultAsync(id);

            if (person == null) return NotFound();

            _bll.People.Remove(person);

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
