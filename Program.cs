using Microsoft.EntityFrameworkCore;
using URLSHORTENER;
using URLSHORTENER.Services;
using URLSHORTENER.ValidationFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options => 
{
    options.Filters.Add<ValidationModelFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ConnectionStr = builder.Configuration.GetConnectionString("DatabaseConnectionStr");
if(builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlite(ConnectionStr));
}
else 
{
     builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlServer(ConnectionStr));
}

builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddScoped<QrGeneratorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();
app.Run();
