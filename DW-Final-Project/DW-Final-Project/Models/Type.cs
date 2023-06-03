using System.ComponentModel.DataAnnotations;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Cargo de um utilizador
	/// </summary>
	public class Type
	{
		public Type() {
			usersList = new HashSet<User>();
		}
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Nome do cargo de um utilizador
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ ]+$", ErrorMessage = "Tem de escrever uma {0} válida")]
        [StringLength(50, ErrorMessage = "A {0} só pode ter apenas {1} caracteres")]
        [Display(Name = "Descrição")]
		public string description { get; set; }

		/// <summary>
		/// Lista de utilizadores com um determinado cargo
		/// </summary>
		public ICollection<User> usersList { get; set; }

	}
}
