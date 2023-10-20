
using BridgeDogs.Data;
using BridgeDogs.Interfaces;
using BridgeDogs.Repository;
using Microsoft.EntityFrameworkCore;

namespace BridgeDogs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Register Repository
            builder.Services.AddScoped<IDogRepository, DogRepository>();

            // Register the database context
            builder.Services.AddDbContext<DogshouseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DogshouseContext"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DogshouseContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}