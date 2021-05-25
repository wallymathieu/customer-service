namespace CustomerService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.Models;

    public sealed class CustomerRepository : ICustomerRepository
    {
        public Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken)
        {
            Database.Customers.Add(customer);
            return Task.FromResult(customer);
        }

        public Task<IQueryable<Customer>> GetCustomersAsync(CancellationToken cancellationToken) =>
            Task.FromResult(Database.Customers.AsQueryable());

        public Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<int> ids, CancellationToken cancellationToken) =>
            Task.FromResult(Database.Customers.Where(x => ids.Contains(x.AccountNumber)));
    }
}
