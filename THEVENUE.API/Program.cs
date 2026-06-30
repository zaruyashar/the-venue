using Scalar.AspNetCore;
using THEVENUE.API.Data;
using THEVENUE.API.Repositories;
using Microsoft.AspNetCore.RateLimiting;

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

builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("ContactApiPolicy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(5);
        opt.PermitLimit = 3;
        opt.SegmentsPerWindow = 5;
        opt.QueueLimit = 0;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
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
app.UseRouting();
app.UseCors("MvcClient");
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();

app.Run();