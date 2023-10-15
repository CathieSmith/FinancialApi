namespace FinancialApi.Services.CustomerService
{
	public interface ICustomerService
	{
		Task<decimal> GetCustomerBalance(int id);
		Task<Customer?> RegistrateCustomer(string surname, string name, string patronymic, string birthDate);
		Task<List<Account>?> UpdateCustomerBalance(int id, decimal money);
	}
}
