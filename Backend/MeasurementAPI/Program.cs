using MeasurementAPI;
using MeasurementAPI.Repositories;
using MeasurementAPI.Services;
using FeatureHubSDK;

var builder = WebApplication.CreateBuilder(args);

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s); //Could add centralized solutions here. 
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s); //Could add centralized solutions here.
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s); //Could add centralized solutions here.
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s); //Could add centralized solutions here.


// Add services to the container.
builder.Services.AddDbContext<Context>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IFeatureToggle, FeatureToggle>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();



using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<Context>();
if (context != null)
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
