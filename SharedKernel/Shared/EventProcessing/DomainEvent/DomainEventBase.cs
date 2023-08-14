using Mc2.CrudTest.Presentation.Shared.SeedWork;
using System;

namespace Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent
{
    [Serializable]
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccurredOn = DateTime.Now;
        }

        public DateTimeOffset OccurredOn { get; }
    }
}
