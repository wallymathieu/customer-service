namespace CustomerService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.Models;

    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken);

        Task<IQueryable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
