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
using Quartz;
using CochainAPI.Jobs;

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

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.WithOrigins(["http://localhost:4200"]).WithMethods(["GET", "POST", "OPTIONS"]).WithHeaders(["Authorization", "Content-Type"])));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadCA", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA", "UserSCP", "UserCA"));
    options.AddPolicy("WriteCA", policy => policy.RequireRole("SystemAdmin"));
    options.AddPolicy("ReadSCP", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA", "UserSCP", "UserCA"));
    options.AddPolicy("WriteSCP", policy => policy.RequireRole("SystemAdmin"));
    options.AddPolicy("AddUser", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA"));
    options.AddPolicy("ReadUser", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA"));
    options.AddPolicy("ReadDocuments", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA", "UserSCP", "UserCA"));
    options.AddPolicy("WriteContracts", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "UserSCP"));
    options.AddPolicy("WriteInvoices", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "UserSCP"));
    options.AddPolicy("WriteTransportDocument", policy => policy.RequireRole("SystemAdmin", "SCPTransporter"));
    options.AddPolicy("WriteOriginDocument", policy => policy.RequireRole("SystemAdmin", "SCPRawMaterialt"));
    options.AddPolicy("WriteCertificationDocument", policy => policy.RequireRole("SystemAdmin", "AdminCA", "UserCA"));
    options.AddPolicy("RemoveCertificationDocument", policy => policy.RequireRole("SystemAdmin", "AdminCA"));
    options.AddPolicy("RemoveDocuments", policy => policy.RequireRole("SystemAdmin"));
    options.AddPolicy("WriteProducts", policy => policy.RequireRole("SystemAdmin", "SCPRawMaterial", "SCPTransformator"));
    options.AddPolicy("WriteProductLifeCycle", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "UserSCP"));
    options.AddPolicy("ReadProducts", policy => policy.RequireRole("SystemAdmin", "AdminSCP", "AdminCA", "UserSCP", "UserCA"));
});

builder.Services.AddDbContext<CochainDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);
builder.Services.AddHttpClient();


builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CochainDBContext>()
                .AddApiEndpoints();

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISupplyChainPartnerService, SupplychainPartnerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductLifeCycleService, ProductLifeCycleService>();
builder.Services.AddSingleton<ICertificationAuthorityService, CertificationAuthorityService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISupplyChainPartnerRepository, SupplyChainPartnerRepository>();
builder.Services.AddSingleton<ICertificationAuthorityRepository, CertificationAuthorityRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductLifeCycleRepository, ProductLifeCycleRepository>();
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("TokenProcessor");
    q.AddJob<TokenProcessor>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("TokenProcessor-trigger")
        .WithSimpleSchedule(x => x.WithIntervalInHours(8).RepeatForever()));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();