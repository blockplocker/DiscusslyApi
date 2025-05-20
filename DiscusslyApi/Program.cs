using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DiscusslyApi.Data;
using DiscusslyApi.DAL;

namespace DiscusslyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DiscusslyApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DiscusslyApiContext") ?? throw new InvalidOperationException("Connection string 'DiscusslyApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<CategoryManeger>();
            builder.Services.AddScoped<PostManeger>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
