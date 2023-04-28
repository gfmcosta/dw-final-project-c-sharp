namespace DW_Final_Project.Models
{
	public class Person
	{
		public int id { get; set; }
		public string name { get; set; }
		public string phoneNumber { get; set; }
		public string address { get; set; }
		public string postalCode { get; set; }
		public DateTime DateTime { get; set; }
		public string gender { get; set; }
		public string imagePath { get; set; }
		//fk
		public User user { get; set; }
		[ForeignKey(nameof(user))]
		public int userFK { get; set; }
	}
}
