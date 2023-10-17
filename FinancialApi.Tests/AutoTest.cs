using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;

namespace FinancialApiTests
{
	public class Tests
	{
		Mock<ICustomerService> _service;
		FinancialApiController _controller;
		Random _rand;

		[SetUp]
		public void Setup()
		{
			_service = new Mock<ICustomerService>();
			_controller = new FinancialApiController(_service.Object);
			_rand = new Random();

			for (int i = 0; i < 50; i++)
			{
				_service.Setup(_service => _service.RegistrateCustomer("Luneva", "Ekaterina", "Evgenevna", "3/6/1998")).ReturnsAsync((Customer)null);
			}
		}
		
		[Test]
		public async Task GetCustomerBalance_IsNotNull()
		{
			// Arrange

			// Act
			var updating = _controller.UpdateCustomerBalance(_rand.Next(1, 49), _rand.Next(1, 1000));

			for (int i = 0; i < 10; i++)
			{
				var task = Task.Run(() => updating);
				await Task.Delay(2000);
			}

			// Assert
			var balance = await _controller.GetCustomerBalance(_rand.Next(1, 49));
			Assert.IsNotNull(balance);
		}
	}
}