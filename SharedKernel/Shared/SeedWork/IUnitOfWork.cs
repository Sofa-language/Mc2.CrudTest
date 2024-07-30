using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
