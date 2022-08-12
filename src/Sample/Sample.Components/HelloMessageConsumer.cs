using MassTransit;
using Sample.Contracts;

namespace Sample.Components
{
    public class HelloMessageConsumer : IConsumer<HelloMessage>
    {
        public async Task Consume(ConsumeContext<HelloMessage> context)
        {
            await context.RespondAsync<HelloMessageAccepted>(new()
            {
                Message = $"Greetings {context.Message.Name}"
            });
        }
    }
}
