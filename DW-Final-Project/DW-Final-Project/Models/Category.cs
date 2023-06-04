using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Categorias dos produtos
	/// </summary>
	public class Category
	{
		public Category() {
			productList = new HashSet<Product>();
		}
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Descrição da Categoria
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Descrição")]
		[RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ -]+$", ErrorMessage = "Tem de escrever uma {0} válida")]
		[StringLength(50,ErrorMessage ="A {0} só pode ter apenas {1} caracteres")]
		public string description { get; set; }

		/// <summary>
		/// Lista dos produtos com uma determinada categoria
		/// </summary>
		public ICollection<Product> productList { get; set; }

	}
}
