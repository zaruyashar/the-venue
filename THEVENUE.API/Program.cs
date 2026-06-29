using Scalar.AspNetCore;
using THEVENUE.API.Data;
using THEVENUE.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MvcClient", policy =>
    {
        policy.WithOrigins(builder.Configuration["ApiSettings:MvcClientUrl"]!)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "THEVENUE API";
        options.Theme = ScalarTheme.Moon;
    });
}

app.UseHttpsRedirection();
app.UseCors("MvcClient");
app.UseAuthorization();
app.MapControllers();

app.Run();