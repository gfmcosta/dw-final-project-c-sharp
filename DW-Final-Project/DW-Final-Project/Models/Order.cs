using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Dados de uma Ordem, i.e, de uma compra feita por um cliente
	/// </summary>
	public class Order
	{
		public Order() {
			orderItemList = new HashSet<OrderItem>();
		}
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Preço total da Ordem/Compra
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço")]
        [RegularExpression("^[0-9.]+$",
         ErrorMessage = "Tem de escrever um {0} válido")]
        public double price { get; set; }


		/// <summary>
		/// FK person
		/// </summary>
		[ForeignKey(nameof(Person))]
		public int personFK { get; set; }
		public Person Person { get; set; }

		/// <summary>
		/// Lista de itens de uma Ordem/Compra
		/// </summary>
		public ICollection<OrderItem> orderItemList { get; set; }
	}
}
