using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Category Name")]
		public required string Name { get; set; }
		[DisplayName("Display Order")]
		public int DisplayOrder { get; set; }
	}
}
