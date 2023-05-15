using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace DW_Final_Project.Models
{
	public class Product_Image
	{
        public int Id { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Imagem")]
		public string imagePath { get; set; }
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
		public Product product { get; set; }
	}
}
