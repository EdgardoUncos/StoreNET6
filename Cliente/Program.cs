using Cliente.DTOs;
using Cliente.Models;

namespace Cliente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Product, ProductDTO>().ReverseMap();
                configuration.CreateMap<Customer, CustomerDTO>().ReverseMap();
                configuration.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                configuration.CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
                configuration.CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
                configuration.CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}