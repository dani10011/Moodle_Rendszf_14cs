using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.WebSockets;
using Moodle.Data;

namespace Moodle.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MoodleDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<DBInit>();


            builder.Services.AddWebSockets(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromMinutes(5); // Set keep-alive interval
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                var dbInit = scope.ServiceProvider.GetRequiredService<DBInit>();
                dbInit.Init().Wait();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(); // Serve static files like HTML, CSS, and JavaScript

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Start WebSocket server in a background task
            
            try
            {
                Task.Run(async () =>
                {
                    var webSocketServer = new WebSocketServer("127.0.0.1", 8000);
                    await webSocketServer.Run();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting WebSocket server: {ex.Message}");
            }
            

            app.Run();
        }
    }
}