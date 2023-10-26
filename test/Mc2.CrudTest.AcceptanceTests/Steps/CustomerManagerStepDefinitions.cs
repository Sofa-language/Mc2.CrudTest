using System;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Models;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Presentation.Shared.Application;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.Steps
{
    [Binding]
    public class CustomerManagerStepDefinitions
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;
        public CustomerManagerStepDefinitions(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"User can create customer")]
        public async Task WhenUserCanCreateCustomer(Table table)
        {
            var createCustomerRequests = table.CreateSet<CreateOrUpdateCustomerModel>();
            var createdCustomerIds = new List<long>();
            foreach (var request in createCustomerRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("customers", request);
                var responseCustomer = await response.Content.ReadFromJsonAsync<long>();
                createdCustomerIds.Add(responseCustomer);
            }
            _scenarioContext.Add("CreatedCustomerIds", createdCustomerIds);
        }

        [Then(@"Operator can see (.*) customer in get user list api result")]
        public void ThenOperatorCanSeeCustomerInGetUserListApiResult(int p0)
        {
            var createdCustomerIds = _scenarioContext.Get<List<long>>("CreatedCustomerIds");

            createdCustomerIds.Count().Should().Be(p0);
        }

        [When(@"Operator can update customer information")]
        public async Task WhenOperatorCanUpdateCustomerInformation(Table table)
        {
            var customerId = _scenarioContext.Get<List<long>>("CreatedCustomerIds").Single();
            var updateCustomerRequest = table.CreateSet<CreateOrUpdateCustomerModel>();
            var response = await _httpClient.PutAsJsonAsync($"customers/{customerId}/", updateCustomerRequest);
            response.IsSuccessStatusCode.Should().Be(true);
        }

        [Then(@"customer data updated successfully")]
        public async Task ThenCustomerDataUpdatedSuccessfully(Table table)
        {
            var customerId = _scenarioContext.Get<List<long>>("CreatedCustomerIds").Single();
            var expectedResult = table.CreateSet<CreateOrUpdateCustomerModel>();
            var response = await _httpClient.GetAsync($"customers/{customerId}/");
            response.IsSuccessStatusCode.Should().Be(true);
            string responseContent = await response.Content.ReadAsStringAsync();
            var convertedModel = JsonConvert.DeserializeObject<CustomerDto>(responseContent);
            convertedModel.Id = customerId;
            convertedModel.Firstname.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().FirstName);
            convertedModel.Lastname.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().LastName);
            convertedModel.DateOfBirth.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().DateOfBirth);
            convertedModel.Email.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().Email);
            convertedModel.PhoneNumber.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().PhoneNumber);
            convertedModel.BankAccountNumber.Should().Be(expectedResult.As<CreateOrUpdateCustomerModel>().BankAccountNumber);

        }

        [When(@"Operator delete customer information")]
        public async Task WhenOperatorDeleteCustomerInformation()
        {
            var customerId = _scenarioContext.Get<List<long>>("CreatedCustomerIds").Single();
            var response = await _httpClient.DeleteAsync($"customers/{customerId}/");
            response.IsSuccessStatusCode.Should().Be(true);
        }

        [Then(@"Customer data delete successfully")]
        public async Task ThenCustomerDataDeleteSuccessfully()
        {
            var customerId = _scenarioContext.Get<List<long>>("CreatedCustomerIds").Single();
            var response = await _httpClient.GetAsync($"customers/{customerId}/");
            response.IsSuccessStatusCode.Should().Be(true);
        }

        [When(@"Operator get list of customer data the item count should be equal to (.*)")]
        public async Task WhenOperatorGetListOfCustomerDataTheItemCountShouldBeEqualTo(int p0)
        {
            var response = await _httpClient.GetAsync("customers/list");
            response.IsSuccessStatusCode.Should().Be(true);
            string responseContent = await response.Content.ReadAsStringAsync();
            var convertedModel = JsonConvert.DeserializeObject<Pagination<CustomerDto>>(responseContent);
            convertedModel.TotalItems.Should().Be(p0);
        }
    }
}
