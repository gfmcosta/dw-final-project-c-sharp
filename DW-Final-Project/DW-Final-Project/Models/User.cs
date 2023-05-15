using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	public class User
	{
		public User() {
			personsList = new HashSet<Person>();
		}

		public int id { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Email")]
		[EmailAddress]
		public string email { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Password")]
		public string password { get; set; }
		public string? token { get; set; }
		//fk
		[ForeignKey(nameof(type))]
		public int typeFK { get; set; }
		public Models.Type type { get; set; }
		public ICollection<Person> personsList { get; set; }
	}
}
