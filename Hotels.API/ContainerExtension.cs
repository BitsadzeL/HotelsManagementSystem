//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;


//namespace University.API
//{
//    public static class ContainerExtension
//    {
//        public static void AddControllers(this WebApplicationBuilder builder)
//        {
//            builder.Services.AddControllers();
//        }
//        public static void AddOpenApi(this WebApplicationBuilder builder)
//        {
//            builder.Services.AddOpenApi();
//        }
//        public static void AddDatabase(this WebApplicationBuilder builder)
//        {
//            builder.Services
//                .AddDbContext<ApplicationDbContext>(options => options
//                .UseSqlServer(builder.Configuration.GetConnectionString("SQLServerLocalConnection"))
//                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
//        }
//        public static void AddAutoMapper(this WebApplicationBuilder builder)
//        {
//            //builder.Services.AddAutoMapper(typeof(MappingProfile));
//        }
//        public static void AddRepositories(this WebApplicationBuilder builder)
//        {

//        }
//        public static void AddServices(this WebApplicationBuilder builder)
//        {
            
//        }
//    }
//}