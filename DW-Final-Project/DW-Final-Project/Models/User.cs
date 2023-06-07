using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Credencias de um utilizador
	/// </summary>
	public class User
	{
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Email de um utilizador
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage ="Insira um {0} válido")]
		public string email { get; set; }

		/// <summary>
		/// Password de um utilizador
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Password")]
		public string password { get; set; }

		/// <summary>
		/// Token (segurança) de um utilizador. Atualizado conforme entrada de um utilizador na aplicação
		/// </summary>
		/// falar com  o professor ainda sobre isto
		public string? token { get; set; }

        /// <summary>
        /// FK Type
        /// </summary>
        [ForeignKey(nameof(type))]
		public int typeFK { get; set; }
		public Models.Type type { get; set; }

	}
}
