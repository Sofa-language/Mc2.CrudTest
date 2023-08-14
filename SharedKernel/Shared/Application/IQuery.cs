using MediatR;

namespace Mc2.CrudTest.Presentation.Shared.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
