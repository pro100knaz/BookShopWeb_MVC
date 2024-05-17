using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		public required string Name { get; set; }

		
		public int DisplayOrder { get; set; }
	}
}
