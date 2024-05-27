using Cliente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class CustomerController : Controller
    {
        private string strCustomer;
        private IConfiguration confi;
        private HttpClient cliente = new HttpClient();

        public CustomerController(IConfiguration configuration)
        {
            confi = configuration;
            strCustomer = confi["EndPoint:UrlCustomers"];
        }
        public async Task<IActionResult> Index()
        {
            var lista = JsonConvert.DeserializeObject<List<CustomerDTO>>(await cliente.GetStringAsync(strCustomer));
            return View("Index", lista);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var urlId = confi["EndPoint:UrlCustomers"] + "/" + id;
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(await cliente.GetStringAsync(urlId));
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDTO customer)
        {
            var urlId = confi["EndPoint:UrlCustomers"] + "/" + customer.Id;
            await cliente.PutAsJsonAsync(urlId, customer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customer)
        {
            var responseMessage = await cliente.PostAsJsonAsync(confi["EndPoint:UrlCustomers"], customer);
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var urlId = confi["EndPoint:UrlCustomers"] + "/" + id;
            await cliente.DeleteAsync(urlId); 
            return RedirectToAction("Index");
        }

        
    }
}
