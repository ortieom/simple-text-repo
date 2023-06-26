using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TextRepo.API;
using TextRepo.API.Tools;
using TextRepo.DataAccessLayer;
using TextRepo.DataAccessLayer.Repositories;
using TextRepo.Services;
using NLog;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Settings location
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// IOptions
builder.Services.AddOptions<DbSettingsModel>().Bind(builder.Configuration.GetSection("Database"));
builder.Services.AddOptions<AuthOptionsModel>().Bind(builder.Configuration.GetSection("JWT"));

// logs
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Logging.ClearProviders();
builder.Logging.AddNLog();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// load everything
builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ProjectService>();
builder.Services.AddTransient<DocumentService>();
builder.Services.AddTransient<ContactService>();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
            ValidateAudience = false,
            IssuerSigningKey = KeyLoader.GetKey(builder.Configuration.GetSection("JWT:KeyLocation").Value),
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true
        }
    );

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