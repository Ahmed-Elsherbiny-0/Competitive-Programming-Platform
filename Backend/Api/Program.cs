


using Application;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependanciesFromInfra(builder.Configuration).AddDependanciesFromApplication(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();


try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
