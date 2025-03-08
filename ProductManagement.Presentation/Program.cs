using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Application;
using ProductManagement.DependencyInjection;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Seed;
using ProductManagement.Presentation.Middleware;
using System.Text;

namespace ProductManagement.Presentation
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);            

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //add mediatR dependency
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            //add dependency to AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationAssemblyReference).Assembly);


            var app = builder.Build();

            //automatic jobs during running app for the first time
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //Auto create database if not exist
                var dbContext = services.GetRequiredService<AppDBContext>();
                await dbContext.Database.MigrateAsync();
                //Add roles and base admin user 
                await IdentitySeeder.SeedAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //exception logger middleware
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
