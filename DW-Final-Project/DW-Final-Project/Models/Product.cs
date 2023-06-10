using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Informações de um produto
	/// </summary>
	public class Product
	{
		public Product() {
			orderItemList = new HashSet<OrderItem>();
			categoryList = new HashSet<Category>();
		}

		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Nome de um produto
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ -]+$", ErrorMessage = "Tem de escrever um {0} válido")]
        [Display(Name = "Nome")]
		public string name { get; set; }

        /// <summary>
        /// Descrição de um produto
        /// </summary>
        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ -.,]+$", ErrorMessage = "Tem de escrever uma {0} válida")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Descrição")]
		public string description { get; set; }

		/// <summary>
		/// Quantidade de um produto
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Quantidade")]
        [Range(0, int.MaxValue, ErrorMessage = "A {0} mínima é 0.")]
        public int quantity { get; set; }

		/// <summary>
		/// Preço unitário de um produto
		/// </summary>
		[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
		[Display(Name = "Preço")]
        public decimal price { get; set; }

        /// <summary>
        /// Campo auxiliar de introdução do Preço de um produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Preço")]
        [RegularExpression("[0-9]+[.,]?[0-9]{1,2}",
           ErrorMessage = "No {0} só pode usar algarismos, e se desejar," +
           " duas casas decimais no final.")]
        [NotMapped]
        public string priceAux { get; set; }
        /// <summary>
        /// Caminho de uma imagem de um produto
        /// </summary>
        public string? imagePath { get; set; }

        /// <summary>
        /// FK Season
        /// </summary>
        [ForeignKey(nameof(Season))]
        public int seasonFK { get; set; }
        public Product_Season Season { get; set; }

        /// <summary>
        /// Lista de produtos numa ordem/compra
        /// </summary>
        public ICollection<OrderItem> orderItemList { get; set; }
		/// <summary>
		/// Lista de categorias de um produto
		/// </summary>
		public ICollection<Category> categoryList { get; set; }
	}
}
