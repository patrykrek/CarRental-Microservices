using app.Services.ReservationAPI.Data;
using app.Services.ReservationAPI.Repository;
using app.Services.ReservationAPI.Repository.Interface;
using app.Services.ReservationAPI.Service;
using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ICarService, CarService>();


SD.CarApiBase = builder.Configuration["ServiceUrls:CarAPI"];
SD.AuthApiBase = builder.Configuration["ServiceUrls:AuthAPI"];

builder.Services.AddHttpClient<ICarService, CarService>();
builder.Services.AddHttpClient<IReservationService, ReservationService>();

builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 31)));
});

var settings = builder.Configuration.GetSection("JwtOptions");

var secret = settings.GetValue<string>("Secret");
var issuer = settings.GetValue<string>("Issuer");
var audience = settings.GetValue<string>("Audience");

var key = Encoding.ASCII.GetBytes(secret);





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Write your JWT token here",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,

    };
});
builder.Services.AddAuthorization();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
