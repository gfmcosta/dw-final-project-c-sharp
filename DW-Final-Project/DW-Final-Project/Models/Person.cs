using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	public class Person
	{
		public Person() {
			orderList = new HashSet<Order>();
		}
		public int id { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Nome")]
		public string name { get; set; }
		[Display(Name = "Telemóvel")]
		[Phone]
		public string phoneNumber { get; set; }
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Morada")]
		public string address { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Código Postal")]
		public string postalCode { get; set; }
		
		[Display(Name = "Data de Nascimento")]
		public DateTime dataNasc { get; set; }
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Sexo")]
		public string gender { get; set; }
		public string imagePath { get; set; }
		//fk
		[ForeignKey(nameof(user))]
		public int userFK { get; set; }
		public User user { get; set; }
		public ICollection<Order> orderList { get; set; }
	}
}
