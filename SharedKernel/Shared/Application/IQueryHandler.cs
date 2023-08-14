using MediatR;

namespace Mc2.CrudTest.Presentation.Shared.Application
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
