using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cliente.DTOs
{
    public class CustomerOrderViewModel: CustomerOrderDTO
    {
        public IEnumerable<SelectListItem> Productos { get; set; }
    }
}
