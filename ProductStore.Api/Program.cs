using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProductStore.Api.Configuration;
using ProductStore.Api.Domain.Mappers;
using ProductStore.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductProfile).Assembly));
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
builder.Services.AddSqlite<DatabaseContext>(builder.Configuration.GetConnectionString("LocalStorage"));

builder.Services.AddIoC(builder.Configuration);
SerilogConfiguration.Configure(builder.Host, builder.Configuration);

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerPage(provider);
app.UseAuthorization();
app.MapControllers();

app.Run();