namespace CustomerService.Resolvers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using CustomerService.Models;
    using CustomerService.Repositories;
    using HotChocolate;

    public class MutationResolver
    {
        public async Task<Customer> CreateCustomerAsync(
            [Service] IImmutableMapper<CustomerInput, Customer> customerInputToCustomerMapper,
            [Service] ICustomerRepository customerRepository,
            CustomerInput customerInput,
            CancellationToken cancellationToken)
        {
            var customer = customerInputToCustomerMapper.Map(customerInput);
            customer = await customerRepository
                .AddCustomerAsync(customer, cancellationToken)
                .ConfigureAwait(false);
            return customer;
        }
    }
}
