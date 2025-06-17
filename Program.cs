using Microsoft.EntityFrameworkCore;
using Product_WebAPI_App.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product WebAPI", Version = "v1" });
});

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PracticeApp")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product WebAPI V1");
        c.RoutePrefix = string.Empty; // Swagger UI at http://localhost:5279/
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files from wwwroot
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
