using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Dados/Informações pessoais de um utilizador
	/// </summary>
	public class Person
	{
		public Person() {
			orderList = new HashSet<Order>();
		}

		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Nome de um utilizador
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Nome")]
        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ ]+$", ErrorMessage = "Tem de escrever um {0} válido")]
        [StringLength(250, ErrorMessage = "O {0} só pode ter apenas {1} caracteres")]
        public string name { get; set; }


		/// <summary>
		/// Nº de telemóvel de um utilizador
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9,
         ErrorMessage = "O {0} tem de ter {1} digitos")]
        [RegularExpression("9[1236][0-9]{7}",
         ErrorMessage = "Tem de escrever um nº de {0} válido")]
        public string phoneNumber { get; set; }


		/// <summary>
		/// Morada de um utilizador
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Morada")]
		public string address { get; set; }


		/// <summary>
		/// Código Postal de um utilizador
		/// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Código Postal")]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-ZÇÁÉÍÓÚ]+[A-Z -ÁÉÍÓÚÇ]*",
         ErrorMessage = "O {0} deve ser escrito no formato XXXX-XXX NOME DA TERRA")]
        public string postalCode { get; set; }


		/// <summary>
		/// Data de Nascimento de um utilizador
		/// </summary>
		[Display(Name = "Data de Nascimento")]
		public DateTime dataNasc { get; set; }



		/// <summary>
		/// Sexo de um utilizador
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Sexo")]
		public string gender { get; set; }

		/// <summary>
		/// Caminho de uma imagem de um utilizador
		/// </summary>
		public string imagePath { get; set; }
		
		/// <summary>
		/// FK User
		/// </summary>
		[ForeignKey(nameof(user))]
		public int userFK { get; set; }
		public User user { get; set; }

		/// <summary>
		/// Lista de ordens/compras de um utilizador
		/// </summary>
		public ICollection<Order> orderList { get; set; }
	}
}
