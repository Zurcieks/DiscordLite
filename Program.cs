using DiscordLite.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidation();

var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
 
 
app.Run();

 