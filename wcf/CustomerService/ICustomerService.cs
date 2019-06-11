using System.ServiceModel;
using System.Threading.Tasks;

namespace Customers
{
    [ServiceContract(ConfigurationName = "ICustomerService")]
    public interface ICustomerService
    {
        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetCustomers",
            ReplyAction = "http://tempuri.org/ICustomerService/GetCustomersResponse")]
        Task<ArrayOfCustomer> GetCustomersAsync(string lastName);
        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetCustomerByAccountNumber", 
            ReplyAction = "http://tempuri.org/ICustomerService/GetCustomerByAccountNumberResponse")]
        Task<Customer> GetCustomerByAccountNumberAsync(int accountNumber);
        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetAllCustomers", 
            ReplyAction = "http://tempuri.org/ICustomerService/GetAllCustomersResponse")]
        Task<ArrayOfCustomer> GetAllCustomersAsync();
        [OperationContract(Action = "http://tempuri.org/ICustomerService/SaveCustomer", 
            ReplyAction = "http://tempuri.org/ICustomerService/SaveCustomerResponse")]
        Task<bool> SaveCustomerAsync(Customer customer);
        [OperationContract(Action = "http://tempuri.org/ICustomerService/SaveCustomerLastName", 
            ReplyAction = "http://tempuri.org/ICustomerService/SaveCustomerLastNameResponse")]
        Task<bool> SaveCustomerLastNameAsync(string accountNumber, string newName);
    }
}
