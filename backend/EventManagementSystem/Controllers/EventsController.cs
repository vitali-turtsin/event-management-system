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
    public class EventsController(IAppBll bll, IMapper mapper, ILogger<EventsController> logger) : ControllerBase
    {
        private readonly IAppBll _bll = bll;
        private readonly IMapper _mapper = mapper;
        private readonly EventMapper _eventMapper = new(mapper);
        private readonly ILogger<EventsController> _logger = logger;

        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvents([FromQuery] EventSearch? search)
        {
            _logger.LogInformation("GetEvents");

            search ??= new EventSearch();

            return (await _bll.Events.FindAllAsync(_mapper.Map<EventSearch, DAL.DTO.Search.EventSearch>(search)))
                .Select(_eventMapper.Map);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            _logger.LogInformation("GetEvent");

            var @event = await _bll.Events.FirstOrDefaultAsync(id);

            if (@event == null) return NotFound();

            return _eventMapper.Map(@event);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _logger.LogInformation("PostEvent");

            var addedEvent = _bll.Events.Add(_eventMapper.Map(@event));

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = addedEvent.Id }, _eventMapper.Map(addedEvent));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            _logger.LogInformation("PutEvent");

            if (id != @event.Id) return BadRequest();

            _bll.Events.Update(_eventMapper.Map(@event));

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            _logger.LogInformation("DeleteEvent");

            var @event = await _bll.Events.FirstOrDefaultAsync(id);

            if (@event == null) return NotFound();

            await _bll.Events.RemoveAsync(id);

            return NoContent();
        }
    }
}
