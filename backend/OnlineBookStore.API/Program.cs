using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineBookStore.API.Data;
using OnlineBookStore.API.Repositories;
using OnlineBookStore.API.Services;
using OnlineBookStore.API.Models;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository ve Service bağımlılıkları
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKitapService, KitapService>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<ISepetRepository, SepetRepository>();
builder.Services.AddScoped<ISepetService, SepetService>();
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>();
builder.Services.AddScoped<ISiparisService, SiparisService>();
builder.Services.AddScoped<OnlineBookStore.API.Services.ITokenService, OnlineBookStore.API.Services.TokenService>();
builder.Services.AddScoped<IYorumRepository, YorumRepository>();
builder.Services.AddScoped<IYorumService, YorumService>();

// CORS ayarı (sadece frontend'e açık)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineBookStore.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Örnek: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// JWT config logu
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<OnlineBookStore.API.Models.JwtSettings>();
Console.WriteLine($"[Program.cs] JWT Key: {jwtSettings.Key}");
Console.WriteLine($"[Program.cs] JWT Issuer: {jwtSettings.Issuer}");
Console.WriteLine($"[Program.cs] JWT Audience: {jwtSettings.Audience}");
builder.Services.AddSingleton(jwtSettings);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
        
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"[JWT Debug] Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"[JWT Debug] Token validated successfully");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

// Geliştirme ortamında Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DbInitializer.Initialize(db);
}

app.MapControllers();

app.Run();

// Not: CSRF, XSS ve SQL Injection'a karşı Entity Framework, JWT, HTTPS ve CORS ile temel koruma sağlanır.
