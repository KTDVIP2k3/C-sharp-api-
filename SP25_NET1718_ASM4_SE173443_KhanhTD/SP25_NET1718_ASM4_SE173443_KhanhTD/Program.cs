
using Azure;
using SP25_NET1718_ASM4_SE173443_KhanhTD.GrapQls;
using zSkinCareBookingRepositories;
using zSkinCareBookingRepositories_;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingRepositories_.Response;
using zSkinCareBookingServices.ImplementService;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.ImplementService;
using zSkinCareBookingServices_.InterfaceService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Query = SP25_NET1718_ASM4_SE173443_KhanhTD.GrapQls.Query;

namespace SP25_NET1718_ASM4_SE173443_KhanhTD
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

            builder.Services.AddGraphQLServer().AddQueryType<Query>().AddType<ResponseObj>()
    .AddType<Schedule>().AddMutationType<Mutation>().BindRuntimeType<DateTime, DateTimeType>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
