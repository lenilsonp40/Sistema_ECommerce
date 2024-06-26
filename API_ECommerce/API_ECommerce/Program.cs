using API_ECommerce.Context;
using API_ECommerce.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
    // options.UseSqlServer("Server=localhost,1433;Database=CodigoDeAutorizacao;User Id=sa;Password=1q2w3e4r@#$;Trust Server Certificate=True;");
    options.UseSqlServer("Data Source=LENILSONNOTE\\SQLEXPRESS;Initial Catalog=ECommerceDB;Integrated Security=True;Trust Server Certificate=True;");

});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();





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
