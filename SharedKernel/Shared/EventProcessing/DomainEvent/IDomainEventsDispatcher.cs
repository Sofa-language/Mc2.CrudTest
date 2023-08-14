using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
