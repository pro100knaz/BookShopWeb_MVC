using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Category Name")]
		[MaxLength(30, ErrorMessage = "The Field is Required")]
		public required string Name { get; set; }
		[DisplayName("Display Order")]
		[Range(1,100, ErrorMessage = "Display Order must be between 1-100")]
		public int DisplayOrder { get; set; }
	}
}
