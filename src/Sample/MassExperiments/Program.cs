using MassExperiment.Contracts;
using MassExperiments.Components;
using MassTransit;

namespace MassExperiments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(t =>
            {
                t.UsingInMemory();
                
                t.AddMediator(m =>
                {
                    m.AddConsumer<SubmitOrderMessageConsumer>();
                    m.AddConsumer<HelloMessageConsumer>();

                    m.AddRequestClient<SubmitOrderMessage>();
                    m.AddRequestClient<HelloMessage>();

                });
            });

            //builder.Services.AddMassTransit(t =>
            //{
            //    t.UsingRabbitMq((context, config) =>
            //    {
            //        config.Host("localhost", "/", c =>
            //        {
            //            c.Username("guest");
            //            c.Password("guest");
            //        });

            //        config.ConfigureEndpoints(context);
            //    });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}