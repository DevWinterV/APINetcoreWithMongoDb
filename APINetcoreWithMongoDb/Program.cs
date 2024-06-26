using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Data;
using APINetcoreWithMongoDb.Fuatures.Account;
using APINetcoreWithMongoDb.Mapping;
using APINetcoreWithMongoDb.Repo;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<IAccountRepo, AccountRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddMassTransit(
    busConfig =>
    {
        busConfig.SetKebabCaseEndpointNameFormatter();
        busConfig.AddConsumer<AccountCreatedConsumer>();
        busConfig.AddConsumer<AccountDeletedConsumer>();
        busConfig.AddConsumer<AccountUpdatedConsumer>();

        busConfig.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
            {
                h.Username(builder.Configuration["MessageBroker:Username"]!);
                h.Password(builder.Configuration["MessageBroker:Password"]!);
            });
            cfg.ConfigureEndpoints(context);
        });
    }
);
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
