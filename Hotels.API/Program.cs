
using Hotel.API;
using Hotels.API.Middleware;
using Hotels.Repository.Data;
using Hotels.Repository.Implementations;
using Hotels.Repository.Interfaces;
using Hotels.Service.Implementations;
using Hotels.Service.Interfaces;
using Hotels.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hotels.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //

            //builder.Services
            //    .AddDbContext<ApplicationDbContext>(options => options
            //    .UseSqlServer(builder.Configuration.GetConnectionString("SQLServerLocalConnection"))
            //    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            //builder.Services.AddAutoMapper(typeof(MappingProfile));

            //builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            //builder.Services.AddScoped<IManagerRepository, ManagerRepository>();


            //builder.Services.AddScoped<IHotelService,HotelService>();
            //builder.Services.AddScoped<IManagerService, ManagerService>();

            //builder.Services.AddScoped<IGuestRepository,GuestRepository>();
            //builder.Services.AddScoped<IGuestService,GuestService>();

            //builder.Services.AddScoped<IRoomRepository,RoomRepository>();
            //builder.Services.AddScoped<IRoomService,RoomService>();

            //builder.Services.AddScoped<IReservationRepository,ReservationRepository>();
            //builder.Services.AddScoped<IReservationService,ReservationService>();

            //builder.Services.AddScoped<IBookingRepository,BookingRepository>();
            //builder.Services.AddScoped<IBookingService,BookingService>();
            builder.AddDatabase();
            builder.AddAutoMapper();
            builder.AddRepositories();
            builder.AddServices();
            builder.AddIdentity();
            builder.ConfigureJwtOptions();
            builder.AddAuthentication();
            //
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.MapOpenApi();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
