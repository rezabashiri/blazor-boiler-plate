using Identity.Api.Backgrounds;
using Identity.Api.StartupSetup;
using Identity.Seeders;
using Identity.StartupSetup;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Authentication and Authorization of Accent", Version = "v1" });
});

builder.Services
    .AddEfIdentity(builder.Configuration, "PostgreAuthenticationConnection")
    .AddOpenIddict(builder.Configuration, builder.Environment)
    .AddScoped<IDatabaseSeeder, IdentitySeeder>()
    .AddHostedService<SeedDatabase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
