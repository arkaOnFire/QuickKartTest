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
		[HttpGet]
		public JsonResult GetProductById(string productId)
		{
			Product product;
			try
			{
				product = repository.GetProductById(productId);
			}
			catch (Exception)
			{
				product = null;
			}
			return Json(product);
		}
		[HttpPost]
		public JsonResult AddProductUsingParams(string productName, byte categoryId, decimal price, int quantityAvailable)
		{
			bool status = false;
			string productId;
			string message;
			try
			{
				status = repository.AddProduct(productName, categoryId, price, quantityAvailable, out productId);
				if (status)
				{
					message = "Successful addition operation, ProductId = " + productId;
				}
				else
				{
					message = "Unsuccessful addition operation!";
				}
			}
			catch (Exception)
			{
				message = "Some error occured, please try again!";
			}
			return Json(message);
		}
		[HttpPost]
		public JsonResult AddProductByModels(Product product)
		{
			bool status = false;
			string message;

			try
			{
				status = repository.AddProduct(product);
				if (status)
				{
					message = "Successful addition operation, ProductId = " + product.ProductId;
				}
				else
				{
					message = "Unsuccessful addition operation!";
				}
			}
			catch (Exception)
			{
				message = "Some error occured, please try again!";
			}
			return Json(message);
		}
		[HttpPut]
		public bool UpdateProductByEFModels(Product product)
		{
			bool status = false;

			try
			{
				status = repository.UpdateProduct(product);
			}
			catch (Exception)
			{
				status = false;
			}
			return status;
		}
	}
}
