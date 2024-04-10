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
    public class PaymentMethodsController(IAppBll bll, IMapper mapper, ILogger<PaymentMethodsController> logger) : ControllerBase
    {
        private readonly IAppBll _bll = bll;
        private readonly IMapper _mapper = mapper;
        private readonly PaymentMethodMapper _eventMapper = new(mapper);
        private readonly ILogger<PaymentMethodsController> _logger = logger;

        [HttpGet]
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods([FromQuery] PaymentMethodSearch? search)
        {
            _logger.LogInformation("GetPaymentMethods");

            search ??= new PaymentMethodSearch();

            return (await _bll.PaymentMethods.FindAllAsync(
                _mapper.Map<PaymentMethodSearch, DAL.DTO.Search.PaymentMethodSearch>(search)))
                .Select(_eventMapper.Map);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            _logger.LogInformation("GetPaymentMethod");

            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null) return NotFound();

            return _eventMapper.Map(paymentMethod);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            _logger.LogInformation("PostPaymentMethod");

            var addedPaymentMethod = _bll.PaymentMethods.Add(_eventMapper.Map(paymentMethod));

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = addedPaymentMethod.Id }, _eventMapper.Map(addedPaymentMethod));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethod paymentMethod)
        {
            _logger.LogInformation("PutPaymentMethod");

            if (id != paymentMethod.Id) return BadRequest();

            _bll.PaymentMethods.Update(_eventMapper.Map(paymentMethod));

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            _logger.LogInformation("DeletePaymentMethod");

            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null) return NotFound();

            _bll.PaymentMethods.Remove(paymentMethod);

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
