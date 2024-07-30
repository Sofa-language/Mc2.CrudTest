using Mc2.CrudTest.Presentation.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Mc2.CrudTest.Presentation.Client.Pages
{
    public partial class EditCustomer
    {
        [Inject]
        ICustomerService CustomerService { get; set; }

        public EditCustomer()
        {
            
        }
    }
}
