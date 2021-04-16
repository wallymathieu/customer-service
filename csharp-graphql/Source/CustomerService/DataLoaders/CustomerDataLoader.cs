namespace CustomerService.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.Models;
    using CustomerService.Repositories;
    using GreenDonut;

    public class CustomerDataLoader : DataLoaderBase<int, Customer>, ICustomerDataLoader
    {
        private readonly ICustomerRepository repository;

        public CustomerDataLoader(IBatchScheduler batchScheduler, ICustomerRepository repository)
            : base(batchScheduler, new DataLoaderOptions<int>()) =>
            this.repository = repository;

        protected override async ValueTask<IReadOnlyList<Result<Customer>>> FetchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var customers = await this.repository.GetCustomersAsync(keys, cancellationToken).ConfigureAwait(false);
            return customers.Select(x => Result<Customer>.Resolve(x)).ToList();
        }
    }
}
