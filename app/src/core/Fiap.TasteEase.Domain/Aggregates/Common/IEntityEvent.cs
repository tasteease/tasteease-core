namespace Fiap.TasteEase.Domain.Aggregates.Common;

public interface IEntityEvent
{
    public string AggregateType { get; init; }
    public string AggregateEventType { get; init; }
    public string Payload { get; init; }
    public DateTime CreatedAt { get; init; }

}