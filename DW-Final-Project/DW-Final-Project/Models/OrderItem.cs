using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	public class OrderItem
	{
		public int id { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Quantidade")]
		public int quantity { get; set; }
		//totalPrice = quantidade * preço/unid
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço Total")]
		public double totalPrice { get; set; }
		//fk
		[ForeignKey(nameof(order))]
		public int orderFK { get; set; }
		public Order order { get; set; }
		//fk
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
		public Product product { get; set; }
	}
}
