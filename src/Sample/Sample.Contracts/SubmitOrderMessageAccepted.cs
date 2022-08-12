namespace Sample.Contracts;

public record SubmitOrderMessageAccepted
{
    public int CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime Timestamp { get; set; }
}