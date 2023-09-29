using BitCoinAnalyzer.API.BackGroundServices;
using BitCoinAnalyzer.API.Helpers;
using BitCoinAnalyzer.Data.DAL;
using BitCoinAnalyzer.Service.Abstract;
using BitCoinAnalyzer.Service.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string MyOrigins = "_myOrigins";

builder.Services.AddControllers();
builder.Services.AddHttpClient();
string connStr = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<BitCoinAnalyzerDbContext>(option => option.UseSqlServer(connStr));
builder.Services.AddScoped<IDatabaseService, EfDatabaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBitCoinService, BitCoinService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyOrigins,
                      policy =>
                      {
                          policy
                          .WithOrigins(appSettings.WebUI_URL)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddHostedService<BitCoinBgService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        c.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Please relogin" }));
                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });
builder.Services.AddAuthorization();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}
