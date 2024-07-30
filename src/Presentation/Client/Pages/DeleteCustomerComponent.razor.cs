using Mc2.CrudTest.Presentation.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Mc2.CrudTest.Presentation.Client.Pages
{
    public partial class DeleteCustomerComponent
    {
        [Inject]
        ICustomerService CustomerService { get; set; }

        public DeleteCustomerComponent()
        {
                
        }
    }
}
