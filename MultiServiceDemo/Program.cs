using MultiServiceDemo.Extention;
using MultiServiceDemo.Help;

namespace MultiServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            var services = builder.Services;
            //自动注入需要注入的
            services.AddSingleton(new Appsettings(configuration));
            services.AddFxServices();


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}