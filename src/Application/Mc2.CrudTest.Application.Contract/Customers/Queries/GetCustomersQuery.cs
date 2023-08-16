using Mc2.CrudTest.Application.Contract.Customers.Dtos;
using Mc2.CrudTest.Presentation.Shared.Application;

namespace Mc2.CrudTest.Application.Contract.Customers.Queries
{
    public class GetCustomersQuery : IQuery<Pagination<CustomerDto>>
    {
        public GetCustomersQuery(int pageSize, int pageCount)
        {
            PageSize = pageSize;
            PageCount = pageCount;
        }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
