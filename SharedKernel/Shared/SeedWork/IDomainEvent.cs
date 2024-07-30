using System;
using MediatR;

namespace Mc2.CrudTest.Presentation.Shared.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTimeOffset OccurredOn { get; }
    }
}
