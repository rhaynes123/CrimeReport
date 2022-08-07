using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Features.Laws;
using Microsoft.EntityFrameworkCore.Cosmos;
using System.Reflection;
using CrimeReport.Features.Violations;

var builder = WebApplication.CreateBuilder(args);
#region
// https://referbruv.com/blog/implementing-cqrs-using-mediator-in-aspnet-core-explained/
#endregion
// Add services to the container.
builder.Services.AddRazorPages();
string key = builder.Configuration["CosmosDb:Key"];
string account = builder.Configuration["CosmosDb:Account"];
string database = builder.Configuration["CosmosDb:DatabaseName"];
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<CrimeDbContext>( options => options.UseCosmos(account, key,database));
builder.Services.AddScoped<ICreateOrUpdateViolationRepository, CreateOrUpdateViolationRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Connection"];
    options.InstanceName = "CrimeReporter";
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CrimeDbContext>();
    await context.Database.EnsureCreatedAsync();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

