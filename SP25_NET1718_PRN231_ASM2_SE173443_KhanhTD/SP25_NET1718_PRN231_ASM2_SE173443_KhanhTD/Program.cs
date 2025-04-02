using SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD.Services;
using zSkinCareBookingRepositories;
using zSkinCareBookingServices.ImplementService;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.ImplementService;
using zSkinCareBookingServices_.InterfaceService;

namespace SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddScoped<ScheduleRepository>();
            builder.Services.AddScoped<TherapistRepository>();
            builder.Services.AddScoped<TherapistServiceInterface, TherapistServiceImplement>();
            builder.Services.AddScoped<ScheduleInterfaceService, ScheduleImplementService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<ScheduleService>();
            app.UseGrpcWeb();  
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}