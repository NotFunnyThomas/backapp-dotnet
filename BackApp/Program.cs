
using BackApp.Model.Repository;
using Domain.Mom;
using Serilog;

namespace BackApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //.WriteTo.Console()
            //.WriteTo.File("logs-backApp.log")
            //.CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            //Add support to logging with SERILOG
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));


            // Add services to the container.
            //builder.Services.AddSerilog(); 
            builder.Services.AddSingleton<CarRepository>();
            builder.Services.AddSingleton<MessageRepository>();
            builder.Services.AddSingleton<MomListener>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSerilogRequestLogging();

            MomListener listener = (MomListener) app.Services.GetRequiredService<MomListener>();
            listener.Initialize();
            listener.Run();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
