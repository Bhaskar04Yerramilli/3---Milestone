using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using JWTNotesAPI.Data;

using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add controller services
builder.Services.AddControllers();


// Swagger configuration with JWT Authentication
builder.Services.AddSwaggerGen(options =>
{
    // Swagger document
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "JWTNotesAPI",
            Version = "v1"
        });

    // JWT Authentication setup
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter JWT Token like: Bearer {your token}"
        });

    // Apply JWT Authentication globally
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});


// Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// JWT Authentication configuration
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                // Validate token issuer
                ValidateIssuer = true,

                // Validate audience
                ValidateAudience = true,

                // Validate token expiry
                ValidateLifetime = true,

                // Validate signing key
                ValidateIssuerSigningKey = true,

                // Valid issuer
                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                // Valid audience
                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                // Secret key
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });


// Authorization service
builder.Services.AddAuthorization();

var app = builder.Build();


// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// HTTPS middleware
app.UseHttpsRedirection();


// Authentication middleware
app.UseAuthentication();


// Authorization middleware
app.UseAuthorization();


// Map controllers
app.MapControllers();

app.Run();