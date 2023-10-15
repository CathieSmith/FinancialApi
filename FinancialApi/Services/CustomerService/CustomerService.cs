using FinancialApi.Models;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinancialApi.Services.CustomerService
{
	public class CustomerService : ICustomerService
	{
		private readonly DataContext _context;
        public CustomerService(DataContext context)
        {
			_context = context;
        }
		async Task<Customer?> ICustomerService.RegistrateCustomer(string surname, string name, string patronymic, string birthDate)
		{
			DateTime dateValue;
			if (!DateTime.TryParse(birthDate, out dateValue))
				return null;

			var customer = new Customer();
			customer.Surname = surname;
			customer.Name = name;
			customer.Patronymic = patronymic;
			customer.BirthDate = dateValue.ToShortDateString();
			_context.Customers.Add(customer);

			var account = new Account();
			account.Customer = customer;
			account.Balance = 0;
			_context.Accounts.Add(account);

			await _context.SaveChangesAsync();

			return await _context.Customers.SingleAsync(x => x.Id == customer.Id);
		}

		async Task<decimal> ICustomerService.GetCustomerBalance(int id)
		{
			var query = await (from c in _context.Customers
						join a in _context.Accounts on c equals a.Customer
						where c.Id == id
						//select new { Surname = c.Surname, Name = c.Name, Patronymic = c.Patronymic, Balance = a.Balance }).ToListAsync();
						select a.Balance).ToListAsync();

			if (query.Count() == 0)
				return -1;

			return query.First();
		}

		async Task<List<Account>?> ICustomerService.UpdateCustomerBalance(int id, decimal money)
		{
			var account = await (from c in _context.Customers
							   join a in _context.Accounts on c equals a.Customer
							   where c.Id == id
							   select a).ToListAsync();

			if (account.Count() == 0)
				return null;

			decimal balance = account.First().Balance;
			balance += money;

			if (balance > 0)
				account.First().Balance = balance;

			await _context.SaveChangesAsync();

			return await (from c in _context.Customers
								join a in _context.Accounts on c equals a.Customer
								where c.Id == id
								select a).ToListAsync();
		}
	}
}
