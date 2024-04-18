using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            //builder.Services.AddSqlServer<MoodleDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
            builder.Services.AddDbContext<MoodleDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<MoodleDbContext>();

            //builder.Services.AddSqlServer<MoodleDbContext>;

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<DBInit>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                //var context = scope.ServiceProvider.GetRequiredService<MoodleDbContext>();
                scope.ServiceProvider.GetRequiredService<DBInit>().Init().Wait();
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