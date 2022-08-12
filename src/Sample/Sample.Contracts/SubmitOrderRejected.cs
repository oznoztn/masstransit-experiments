namespace Sample.Contracts;

public record SubmitOrderRejected
{
    public string Reason { get; set; }
}