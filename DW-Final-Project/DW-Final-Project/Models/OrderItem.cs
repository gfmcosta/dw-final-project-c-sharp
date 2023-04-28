namespace DW_Final_Project.Models
{
	public class OrderItem
	{
		public int id { get; set; }
		public int quantity { get; set; }
		//totalPrice = quantidade * preço/unid
		public double totalPrice { get; set; }
		//fk
		public Order order { get; set; }
		[ForeignKey(nameof(order))]
		public int orderFK { get; set; }
		//fk
		public Product product { get; set; }
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
	}
}
