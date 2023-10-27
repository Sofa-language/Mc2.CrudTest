using Mc2.CrudTest.Presentation.Client.Models;

namespace Mc2.CrudTest.Presentation.Client.Services
{
    public class CustomerService
    {
        public static Customer[] GetCustomerList()
        {
            return Customers.ToArray();
        }

        public static void AddCustomer(Customer customer)
        {
            customer.Id = Customers.Max(c => c.Id)+1;
            Customers.Add(customer);
        }

        public static Customer GetCustomerById(long id)
        {
            return Customers.Find(c => c.Id == id) ?? throw new Exception("Could not find customer");
        }

        public static void UpdateCustomer(Customer customer)
        {
            Customer existingCustomer = GetCustomerById(customer.Id);
            existingCustomer.BankAccountNumber = customer.BankAccountNumber;
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.Email = customer.Email;
            existingCustomer.Firstname = customer.Firstname;
            existingCustomer.Lastname = customer.Lastname;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
        }


        public static void DeleteCustomer(long id)
        {
            Customer customer = GetCustomerById(id);
            Customers.Remove(customer);
        }
        private static readonly List<Customer> Customers = new()
        {
            new Customer()
            {
                BankAccountNumber = "178451216546",
                DateOfBirth = new DateTimeOffset(1988, 09, 07, 0, 0, 0, new TimeSpan(3, 30, 0)),
                Email = "jahanbin.ali1988@gmail.com",
                Firstname = "Ali",
                Lastname = "Jahanbin",
                PhoneNumber = "+989224957626",
                Id = 1
            },
            new Customer()
            {
                BankAccountNumber = "178415216546",
                DateOfBirth = new DateTimeOffset(1988, 07, 09, 0, 0, 0, new TimeSpan(3, 30, 0)),
                Email = "ali.jahanbin1988@gmail.com",
                Firstname = "Jahanbin",
                Lastname = "Ali",
                PhoneNumber = "+989165770704",
                Id = 2
            }

        };
    }
}
