using System.Security.Principal;

namespace DW_Final_Project.Models
{
	public class Product_Image
	{
        public int Id { get; set; }
		public string imagePath { get; set; }
		//fk
		public int product { get; set; }
    }
}
