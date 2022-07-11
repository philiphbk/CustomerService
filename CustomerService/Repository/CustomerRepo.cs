using CustomerService.Data;
using CustomerService.Model;

namespace CustomerService.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CustomerDbContext _customerDb;

        public CustomerRepo(CustomerDbContext customerDb)
        {
            _customerDb = customerDb;
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _customerDb.Customers.Add(customer);
        }

        public void DeleteCustomerById(int id)
        {
            /*_customerDb.Customers.Remove(Convert.ToString(id));*/
            throw new NotImplementedException();
        }

        public void DeleteCustomerByName(string name)
        {
            /*_customerDb.Customers.Remove(name);*/
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerDb.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerDb.Customers.FirstOrDefault(x => x.Id.Equals(id));
        }

        public Customer GetCustomerByName(string name)
        {
            return _customerDb.Customers.Find(name);
        }

        public bool SaveChanges()
        {
            return (_customerDb.SaveChanges() >= 0);
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
