using Microsoft.EntityFrameworkCore;
using StoreNET6.Data;
using StoreNET6.DTOs;
using StoreNET6.Models;
using System.Text.Json;

namespace StoreNET6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string strCon = builder.Configuration.GetConnectionString("CadenaSQL");
            builder.Services.AddDbContext<StoreDBContext>(opt => opt.UseSqlServer(strCon));

            builder.Services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Product, ProductDTO>().ReverseMap();
                configuration.CreateMap<Customer, CustomerDTO>().ReverseMap();
                configuration.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                configuration.CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
                configuration.CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
                configuration.CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
