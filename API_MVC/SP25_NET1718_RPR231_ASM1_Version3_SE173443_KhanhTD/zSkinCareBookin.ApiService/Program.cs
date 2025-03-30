
using System.Text.Json.Serialization;
using zSkinCareBookingRepositories;
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
			builder.Services.AddScoped<ScheduleInterfaceService, ScheduleImplementService>();
			builder.Services.AddScoped<TherapistServiceInterface, TherapistServiceImplement>();

			builder.Services.AddControllers().AddJsonOptions(option =>
			{
				option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
