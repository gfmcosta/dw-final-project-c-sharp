using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Caminhos das imagens de um produto
	/// </summary>
	public class Product_Image
	{
		/// <summary>
		/// PK
		/// </summary>
        public int Id { get; set; }

		/// <summary>
		/// Caminho de uma imagem de um produto
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Imagem")]
		public string imagePath { get; set; }

		/// <summary>
		/// FK Product
		/// </summary>
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
		public Product product { get; set; }
	}
}
