using System.ComponentModel.DataAnnotations;

namespace DW_Final_Project.Models
{
	public class Product
	{
		public Product() {
			orderItemList = new HashSet<OrderItem>();
			productImageList = new HashSet<Product_Image>();
			categoryList = new HashSet<Category>();
		}
		public int id { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Nome")]
		public string name { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Descrição")]
		public string description { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Quantidade")]
		public int quantity { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço")]
		public double price { get; set; }
		public ICollection<OrderItem> orderItemList { get; set; }
		public ICollection<Product_Image> productImageList { get; set; }
		public ICollection<Category> categoryList { get; set; }


	}
}
