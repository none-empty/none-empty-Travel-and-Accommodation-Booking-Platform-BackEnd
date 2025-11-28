using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform.DB.AppDbContextFiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetSection("connectionStrings")?["sqlserver"])
);
 
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

 
app.UseHttpsRedirection();

 
 

app.Run();

 