using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinancialApiTests
{
	public class Tests
	{
		Mock<ICustomerService> _service;

		[SetUp]
		public void Setup()
		{
			_service = new Mock<ICustomerService>();
		}

		[Test]
		public void Test()
		{
			// Arrange
			var controller = new FinancialApiController(_service.Object);
			for (int i = 0; i < 50; i++)
			{
				_service.Setup(_service => _service.RegistrateCustomer("Luneva", "Ekaterina", "Evgenevna", "3/6/1998")).ReturnsAsync((Customer)null);
			}
			/*
			// Act
			List<Thread> threads = new List<Thread>();
			for (int i = 0; i < 10; i++)
			{
				Thread thread = new Thread(controller.UpdateCustomerBalance(, 20));
				threads.Add(thread);
			}
			foreach (var t in threads)
			{
				t.Start();
			}

			// Assert
			Assert.Equals(controller.GetCustomerBalance(1), 20);*/
		}
	}
}