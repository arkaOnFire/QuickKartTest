using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace QuickKartServices.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : Controller
	{
		QuickKartRepository repository;
		public ProductController(QuickKartRepository repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public JsonResult GetAllProducts()
		{
			List<Product> products = new List<Product>();
			try
			{
				products = repository.GetAllProducts();
			}
			catch (Exception)
			{
				products = null;
			}
			return Json(products);
		}


	}
}
