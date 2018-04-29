namespace Customers
{
    public interface ICustomerService
    {
        ArrayOfCustomer GetCustomers(string lastName);

        Customer GetCustomerByAccountNumber(int accountNumber);

        ArrayOfCustomer GetAllCustomers();

        bool SaveCustomer(Customer editedCustomer);

        bool SaveCustomerLastName(string accountNumber, string newName);
    }
}
