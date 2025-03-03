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
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Data.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Auth.AspNetCore3;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("CochainDB")!;

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAuthentication()
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
        RequireSignedTokens = true
    };
})
.AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadDocuments", policy => policy.RequireRole("Admin", "User"));
    options.AddPolicy("WriteDocuments", policy => policy.RequireRole("Admin"));
});

builder.Services.AddDbContext<CochainDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);
builder.Services.AddHttpClient();


builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<CochainDBContext>()
                .AddApiEndpoints();

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISupplyChainPartnerService, SupplychainPartnerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISupplyChainPartnerRepository, SupplyChainPartnerRepository>();
builder.Services.AddSingleton<IContractRepository, ContractRepository>();
builder.Services.AddSingleton<IProductLifeCycleDocumentRepository, ProductLifeCycleDocumentRepository>();
builder.Services.AddSingleton<ISupplyChainPartnerCertificateRepository, SupplyChainPartnerCertificateRepository>();


builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
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