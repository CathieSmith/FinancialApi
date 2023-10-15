using FinancialApi.Models;
using FinancialApi.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FinancialApiController : ControllerBase
	{
		private readonly ICustomerService _customerService;
        public FinancialApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<decimal>> GetCustomerBalance(int id)
		{
			var result = await _customerService.GetCustomerBalance(id);

			if (result == -1)
				return NotFound("Customer with this ID is not found");

			return Ok(string.Format("Balance for customer with ID {0}: {1}", id, result));
		}
		[HttpPost]
		public async Task<ActionResult<Customer>> RegistrateCustomer(string surname, string name, string patronymic, string birthDate)
		{
			var result = await _customerService.RegistrateCustomer(surname, name, patronymic, birthDate);

			if (result is null)
				return BadRequest("BirthDate is not correct");

			return Created("", result);
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<Account>> UpdateCustomerBalance(int id, decimal money)
		{
			var result = await _customerService.UpdateCustomerBalance(id, money);

			if (result is null)
				return NotFound("Customer with this ID is not found");

			return Ok(result);
		}
	}
}
