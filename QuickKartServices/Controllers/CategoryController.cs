using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace QuickKartServices.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CategoryController : Controller
	{
		QuickKartRepository repository;
		public CategoryController(QuickKartRepository repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public JsonResult GetCategories()
		{
			List<Category> categories = new List<Category>();
			try
			{
				categories = repository.GetAllCategories();
			}
			catch (Exception)
			{
				categories = null;
			}
			return Json(categories);
		}
	}
}
