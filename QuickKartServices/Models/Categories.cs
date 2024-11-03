using System.ComponentModel.DataAnnotations;

namespace QuickKartServices.Models
{
	public class Categories
	{
		[Required]
		public byte CategoryId { get; set; }
		[Required]
		public string CategoryName { get; set; } = null!;
	}
}
