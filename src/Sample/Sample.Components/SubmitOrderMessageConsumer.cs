using MassTransit;
using Sample.Contracts;

namespace Sample.Components
{
    public class SubmitOrderMessageConsumer : IConsumer<SubmitOrderMessage>
    {
        public async Task Consume(ConsumeContext<SubmitOrderMessage> context)
        {
            if (context.Message.CustomerId == default)
            {
                await context.RespondAsync<SubmitOrderRejected>(new()
                {
                    Reason =  $"Given id cannot be 0."
                });

                return;
            }

            await context.RespondAsync<SubmitOrderMessageAccepted>(new()
            {
                CustomerId = context.Message.CustomerId,
                OrderId = Guid.NewGuid(),
                Timestamp = InVar.Timestamp
            });
        }
    }
}