using CustomerService.Model;

namespace CustomerService.Repository
{
    public interface ICustomerRepo
    {
        bool SaveChanges();

        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomerById(int id);

        Customer GetCustomerByName(string name);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomerByName(string name);
    }
}
