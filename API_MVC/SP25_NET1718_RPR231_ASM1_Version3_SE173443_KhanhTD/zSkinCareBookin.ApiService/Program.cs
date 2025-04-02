
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using zSkinCareBookingRepositories;
using zSkinCareBookingRepositories_;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices.ImplementService;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.ImplementService;
using zSkinCareBookingServices_.InterfaceService;

namespace zSkinCareBookin.ApiService_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        
            builder.Services.AddScoped<ScheduleRepository>();
            builder.Services.AddScoped<TherapistRepository>();
            builder.Services.AddScoped<UserAccountRepository>();
            builder.Services.AddScoped<ScheduleInterfaceService, ScheduleImplementService>();
            builder.Services.AddScoped<TherapistServiceInterface, TherapistServiceImplement>();
            builder.Services.AddScoped<UserAccountServiceInterface, UserAccountServiceImplement>();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
                using (var sha256 = SHA256.Create())
                {
                    key = sha256.ComputeHash(key);
                }

                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

            });

            builder.Services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            });


            static IEdmModel GetEdmModel()
            {
                var odataBuilder = new ODataConventionModelBuilder();
                odataBuilder.EntitySet<ServiceCategory>("Therapist"); // EDM - ENTITY DATA MODEL
                odataBuilder.EntitySet<Service>("Schedule"); // ENTITY
                return odataBuilder.GetEdmModel();
            }
            builder.Services.AddControllers().AddOData(options =>
            {
                options.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
                options.AddRouteComponents("odata", GetEdmModel());
            });


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(option =>
            {
         
                option.DescribeAllParametersInCamelCase();
                option.ResolveConflictingActions(conf => conf.First());
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
        }
    }
}
