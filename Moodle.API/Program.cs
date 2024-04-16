using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moodle.Data;

namespace Moodle.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<MoodleDbContext>();
                scope.ServiceProvider.GetService<DBInit>().Init();
            }
            app.Services.CreateScope().ServiceProvider.GetService<MoodleDbContext>();

            app.UseHttpsRedirection();

            app.UseStaticFiles(); // Serve static files like HTML, CSS, and JavaScript

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}