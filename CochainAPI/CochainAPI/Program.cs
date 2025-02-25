using CochainAPI.Authentication.Interfaces;
using CochainAPI.Authentication;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Services;
using CochainAPI.Helpers;
using CochainAPI.Model.Authentication;
using CochainAPI.Model.DTOs.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CochainAPI.Data.Sql;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("CochainDB")!;

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt")["ValidIssuer"],
                    ValidAudience = builder.Configuration.GetSection("Jwt")["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt")["Secret"])),
                    RequireSignedTokens = true
                };
            });
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder();


builder.Services.AddDbContext<CochainDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
builder.Services.AddHttpClient();


builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<CochainDBContext>()
                .AddApiEndpoints();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();


//builder.Services.AddTransient<IOurHeroService, OurHeroService>();
//builder.Services.AddScoped<IOurHeroService, OurHeroService>();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();