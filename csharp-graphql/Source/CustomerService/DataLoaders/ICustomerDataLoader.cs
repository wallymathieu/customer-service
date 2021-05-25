namespace CustomerService.DataLoaders
{
    using System;
    using CustomerService.Models;
    using GreenDonut;

    public interface ICustomerDataLoader : IDataLoader<int, Customer>
    {
    }
}
