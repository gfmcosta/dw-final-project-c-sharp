using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Detalhes de um item, numa determinada Ordem/Compra
	/// </summary>
	public class OrderItem
	{
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Quantidade de um item numa ordem/compra
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Quantidade")]
		public int quantity { get; set; }

		/// <summary>
		/// Preço total de um determinado item numa ordem/compra (totalPrice = quantidade * preco/unid
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço Total")]
        [RegularExpression("^[0-9.]+$", ErrorMessage = "Tem de escrever um {0} válido")]
        public double totalPrice { get; set; }
		

		/// <summary>
		/// FK Order
		/// </summary>
		[ForeignKey(nameof(order))]
		public int orderFK { get; set; }
		public Order order { get; set; }
		
		/// <summary>
		/// FK produto
		/// </summary>
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
		public Product product { get; set; }
	}
}
