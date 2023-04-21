namespace DW_Final_Project.Models
{
	public class Order
	{
		public int id { get; set; }
		//preço da conta toda
		public double price { get; set; }
		//fk
		public int user { get; set; }
	}
}
