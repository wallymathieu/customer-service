namespace CustomerService.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.DataLoaders;
    using CustomerService.Models;
    using CustomerService.Repositories;
    using HotChocolate;

    public class QueryResolver
    {
        public Task<IQueryable<Customer>> GetCustomersAsync(
            [Service] ICustomerRepository customerRepository,
            CancellationToken cancellationToken) =>
            customerRepository.GetCustomersAsync(cancellationToken);

        public Task<IReadOnlyList<Customer>> GetCustomersByIdsAsync(
            ICustomerDataLoader customerDataLoader,
            List<int> ids,
            CancellationToken cancellationToken) =>
            customerDataLoader.LoadAsync(ids, cancellationToken);
    }
}
