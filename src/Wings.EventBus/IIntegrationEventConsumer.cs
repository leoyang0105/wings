using MassTransit;

namespace Wings.EventBus
{
    public interface IIntegrationEventConsumer<in TIntegrationEvent> : IConsumer<TIntegrationEvent>
         where TIntegrationEvent : IntegrationEvent
    {
    }
}
