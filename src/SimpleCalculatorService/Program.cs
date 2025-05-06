using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SimpleCalculatorService.Security;
using SimpleCalculatorService.Services;


namespace SimpleCalculatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Initialize the web host
                var builder = WebApplication.CreateBuilder(args);

                // Load environment variables
                if (builder.Environment.IsDevelopment())
                {
                    DotNetEnv.Env.Load(); // Load from .env file (for development)
                }
                builder.Configuration.AddEnvironmentVariables(); // Add environment variables

                // Add services to the container
                builder.Services.AddControllers();

                // Configure JWT Authentication
                var jwtKey = builder.Configuration["Jwt:Key"];
                //var jwtIssuer = builder.Configuration["Jwt:Issuer"];
                //var jwtAudience = builder.Configuration["Jwt:Audience"];

                if (string.IsNullOrEmpty(jwtKey))
                {
                    throw new Exception("JWT Key is not set in the environment variables.");
                }

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });

                // Register application services
                builder.Services.AddSingleton<IJwtService, JwtService>();
                builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

                // Add Swagger services
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Simple Calculator Service API",
                        Version = "v1",
                        Description = "API documentation for the Simple Calculator Service"
                    });

                    // Add JWT Authentication to Swagger
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter your token in the text input below.\nExample: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
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

                var app = builder.Build();

                // Configure the HTTP request pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();

                    // Enable Swagger in development
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Calculator Service API v1");
                        c.RoutePrefix = "swagger"; // string.Empty; // Set Swagger UI at the root
                    });
                }

                app.UseRouting();
                app.UseAuthentication(); // Add authentication middleware
                app.UseAuthorization(); // Add authorization middleware
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
