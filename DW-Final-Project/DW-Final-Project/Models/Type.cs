using System.ComponentModel.DataAnnotations;

namespace DW_Final_Project.Models
{
	public class Type
	{
		public Type() {
			usersList = new HashSet<User>();
		}

		public int id { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Descrição")]
		public string description { get; set; }
		public ICollection<User> usersList { get; set; }

	}
}
