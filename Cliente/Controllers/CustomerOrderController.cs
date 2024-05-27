using Cliente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly string UrlCustomers;
        private readonly string UrlProducts;
        private readonly string UrlOrderStatuses;
        private readonly string UrlCustomerOrder;
        private readonly HttpClient client = new HttpClient();
        public CustomerOrderController(IConfiguration confi)
        {
            UrlCustomers = confi["EndPoint:UrlCustomers"];
            UrlProducts = confi["EndPoint:UrlProducts"];
            UrlOrderStatuses = confi["EndPoint:UrlOrderStatuses"];
            UrlCustomerOrder = confi["EndPoint:UrlCustomerOrders"];
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewOrder(int id)
        {
            CustomerOrderDTO dto = new CustomerOrderDTO();
            var urlID = UrlCustomers + "/" + id;
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(await client.GetStringAsync(urlID));
            dto.Customer = customer;
            dto.CustomerId = customer.Id;
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(await client.GetStringAsync(UrlProducts));

            CustomerOrderDTO customerOrderDTO = new CustomerOrderDTO();
            customerOrderDTO.CustomerId = customer.Id;
            customerOrderDTO.OrderStatusId = 1;
            customerOrderDTO.Date = DateTime.Now;
            customerOrderDTO.Amount = 0;

            //var result = JsonConvert.DeserializeObject<CustomerOrderDTO>(await client.GetStringAsync(UrlCustomerOrder));



            ViewBag.Products = products.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                    Selected = false
                };
            });
            
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder([FromBody] CustomerOrderDTO order)
        {
            var result = await client.PostAsJsonAsync(UrlCustomerOrder, order);

            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPrecio([FromBody] int id)
        {
            var urlID = UrlProducts + "/" + id;
            var product = JsonConvert.DeserializeObject<ProductDTO>(await client.GetStringAsync(urlID));

            return Ok(product.UnitPrice);
        }

    }
}
