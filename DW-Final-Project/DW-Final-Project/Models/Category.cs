using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DW_Final_Project.Models
{
	public class Category
	{
		public Category() {
			productList = new HashSet<Product>();
		}
		public int id { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Descrição")]
		public string description { get; set; }
		public ICollection<Product> productList { get; set; }

	}
}
