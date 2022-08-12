namespace Sample.Contracts;

public record SubmitOrderMessage
{
    public int CustomerId { get; set; }
}