using FastEndpoints;
using FastEndpoints.Swagger;
using ResortManager.Services.Finance.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFastEndpoints()
        .SwaggerDocument(o =>
        {
            o.ShortSchemaNames = true;
            o.DocumentSettings = s =>
            {
                s.Title = "RM Finance API";
                s.Description = "API for managing finances.";
                s.Version = "v1";
            };
        });

builder.Services.AddMediatorServices();

builder.Services.AddDatabases(builder.Configuration, builder.Environment);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.RunAsync();

