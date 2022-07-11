using CustomerService.Data;
using CustomerService.Model;
using CustomerService.Repository;
using CustomerService.Seeder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*using Microsoft.OpenApi.Models;*/


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
/*builder.Services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(connectionString));*/
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title ="CustomerService", Version = "v1" });
});*/
var app = builder.Build();


if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

/*app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});*/

app.MapGet("/", () => "Hello World!");

app.MapGet("/customer/{id}", ([FromServicesAttribute] CustomerDbContext db, string id) =>
{
    return db.Customers.Where(x => x.Id == id).FirstOrDefault();
});

app.MapGet("/customers", ([FromServicesAttribute] CustomerDbContext db) =>
{
    return db.Customers.ToList();
});

app.MapPut("/customer/{id}", ([FromServicesAttribute] CustomerDbContext db, Customer customer) =>
{
    db.Customers.Update(customer);
    db.SaveChanges();
    return db.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
});

app.MapPost("/customer", ([FromServicesAttribute] CustomerDbContext db, Customer customer) =>
{
    db.Customers.Add(customer);
    db.SaveChanges();
    return db.Customers.ToList();
});


app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
