using Cliente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class ProductController : Controller
    {
        private string urlProduct;
        private HttpClient client = new HttpClient();

        public ProductController(IConfiguration confi)
        {
            urlProduct = confi["EndPoint:UrlProducts"];
        }
        public async Task<IActionResult> Index()
        {
            var lista = JsonConvert.DeserializeObject<List<ProductDTO>>(await client.GetStringAsync(urlProduct));
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            var responseMessage = await client.PostAsJsonAsync(urlProduct, product);
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var urlID = urlProduct + "/" + id;
            var producto = JsonConvert.DeserializeObject<ProductDTO>(await client.GetStringAsync(urlID));

            return View(producto);
        }
    }
}
