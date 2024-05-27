using Cliente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration confi;
        HttpClient cliente = new HttpClient();

        public EmployeeController(IConfiguration confi)
        {
            this.confi = confi;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var url = confi["EndPoint:UrlEmployees"];
            var lista = JsonConvert.DeserializeObject<List<EmployeeDTO>>(await cliente.GetStringAsync(url));
            return View(lista);
        }

        [HttpGet]
        public async Task <IActionResult> Edit(int id)
        {
            var urlId = confi["EndPoint:UrlEmployees"] + "/" + id;
            var employee = JsonConvert.DeserializeObject<EmployeeDTO>(await cliente.GetStringAsync(urlId));
            return View(employee);
        }

        [HttpPost]
        public async Task <IActionResult> Edit(EmployeeDTO employeeDTO)
        {
            var urlID = confi["EndPoint:UrlEmployees"] + "/" + employeeDTO.Id;
            await cliente.PutAsJsonAsync(urlID, employeeDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            var responseMessage = await cliente.PostAsJsonAsync(confi["EndPoint:UrlEmployees"], employeeDTO);
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            return RedirectToAction("Index");   
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var urlID = confi["EndPoint:UrlEmployees"] + "/" + id;
            await cliente.DeleteAsync(urlID);
            return RedirectToAction("Index");
        }
    }
}
