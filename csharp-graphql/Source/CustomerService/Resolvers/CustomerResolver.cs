namespace CustomerService.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.DataLoaders;
    using CustomerService.Models;
    using CustomerService.Repositories;
    using HotChocolate;

    public class CustomerResolver
    {
        public Task<Customer> GetCustomerAsync(ICustomerDataLoader customerDataLoader, int id, CancellationToken cancellationToken) =>
            customerDataLoader.LoadAsync(id, cancellationToken);

    }
}
