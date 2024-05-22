
using DataAccessLayer.Repository;
using DataAccessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Services
            builder.Services.AddScoped(typeof(ITestService), typeof(TestService));
            builder.Services.AddScoped(typeof(IProdusService), typeof(ProdusService));
            builder.Services.AddScoped(typeof(IFurnizorService), typeof(FurnizorService));
            builder.Services.AddScoped(typeof(IDepartamentService), typeof(DepartamentService));
            builder.Services.AddScoped(typeof(IComandaService), typeof(ComandaService));
            builder.Services.AddScoped(typeof(IProdusComandaService), typeof(ProdusComandaService));


            
            
            

            //Database
            builder.Services.AddDbContext<MyDbContext>();

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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<MyDbContext>();

                
                dbContext.Database.Migrate();

                // Apelul metodei SeedData
                dbContext.SeedData();
            }

            app.Run();
        }
    }
}
