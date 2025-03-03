
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application;
using ProductManagement.Infrastructure.IOC;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Presentation.Middleware;

namespace ProductManagement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //add mediatR dependency
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            //add dependency to AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationAssemblyReference).Assembly);


            var app = builder.Build();

            //automatic create databse and tables after running project
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDBContext>();
                dbContext.Database.Migrate(); 
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
