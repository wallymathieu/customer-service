namespace CustomerService.Mappers
{
    using System;
    using Boxed.Mapping;
    using CustomerService.Models;
    using CustomerService.Services;

    public class CustomerInputToCustomerMapper : IImmutableMapper<CustomerInput, Customer>
    {
        private readonly IClockService clockService;

        public CustomerInputToCustomerMapper(IClockService clockService) => this.clockService = clockService;

        public Customer Map(CustomerInput source)
        {
            var now = this.clockService.UtcNow;

            var customer = new Customer(
                Address: source.Address,
                AccountNumber: -1,
                Name: source.Name,
                Gender: source.Gender,
                PictureUri: source.PictureUri,
                Added: now);

            return customer;
        }
    }
}
