using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	public class Order
	{
		public Order() {
			orderItemList = new HashSet<OrderItem>();
		}
		public int id { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço")]
		public double price { get; set; }

		[ForeignKey(nameof(person))]
		public int personFK { get; set; }
		public Person person { get; set; }
		public ICollection<OrderItem> orderItemList { get; set; }
	}
}
