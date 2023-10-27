using System.Net.Http.Json;
using Mc2.CrudTest.Presentation.Client.Models;
using Mc2.CrudTest.Presentation.Shared.Application;
using Newtonsoft.Json;

namespace Mc2.CrudTest.Presentation.Client.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:4434";
        public CustomerService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<Customer[]> GetCustomerListAsync()
        {
            var result = new Pagination<Customer>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/customers/list");
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<Pagination<Customer>>(json);

                return res!.TotalItems == 0 ? Array.Empty<Customer>() : res.Items.ToArray()!;
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }
            return Array.Empty<Customer>();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/customers", customer);
                var json = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<long>(json);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to the server");
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"{baseUrl}/customers/{id}") ?? throw new Exception("Could not find customer"); ;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/customers/{customer.Id}", customer);
                var json = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<long>(json);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            //string serializedData = JsonConvert.SerializeObject(customer);
            //HttpContent content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = null;
            //try
            //{
            //    response = await _httpClient.PutAsync($"{baseUrl}/customers/{customer.Id}", content);
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        throw new Exception();
            //    }
            //}
            //catch
            //{
            //    throw new Exception("Unable to connect to the server");
            //}
        }


        public async Task DeleteCustomerAsync(long id)
        {
            await _httpClient.DeleteAsync($"{baseUrl}/customers/{id}");
        }
    }
}
