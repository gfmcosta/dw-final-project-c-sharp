namespace DW_Final_Project.Models
{
	public class OrderItem
	{
		public int id { get; set; }
		public int quantity { get; set; }
		//totalPrice = quantidade * preço/unid
		public double totalPrice { get; set; }
		//fk
		public int order { get; set; }
		//fk
		public int produto { get; set; }
	}
}
