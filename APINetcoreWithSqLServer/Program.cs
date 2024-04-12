using APINetcoreWithSqLServer.@abstract;
using APINetcoreWithSqLServer.Data;
using APINetcoreWithSqLServer.Mapping;
using APINetcoreWithSqLServer.Repo;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Connect DB

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLDbConnections"))
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountRepo, AccountRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddMassTransit(
    busConfig =>
    {
        busConfig.SetKebabCaseEndpointNameFormatter();
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
