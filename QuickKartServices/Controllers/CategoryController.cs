using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;
using QuickKartServices.Models;

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
		[HttpPost]
		public Boolean AddCategory(Category category)
		{
			bool status = false;
			try
			{
				status = repository.AddCategory(category);
			}
			catch (Exception)
			{
				status = false;
			}
			return status;
		}
		[HttpPut]
		public Boolean UpdateCategory(Models.Categories categories)
		{
			bool status = false;
			try
			{
				if(ModelState.IsValid)
				{
					status = repository.UpdateCategory(categories.CategoryId, categories.CategoryName);
				}
			}
			catch (Exception)
			{
				status = false;
			}
			return status;
		}
		[HttpDelete]
		public Boolean DeleteCategory(byte categoryId)
		{
			bool status = false;
			try
			{
				if (categoryId!=0)
				{
					status = repository.DeleteCategory(categoryId);
				}
				else
					status = false;
			}
			catch (Exception)
			{
				status = false;
			}
			return status;
		}
	}
}