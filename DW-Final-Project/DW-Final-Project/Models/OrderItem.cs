using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Detalhes de um item, numa determinada Ordem/Compra
	/// </summary>
	public class OrderItem
	{
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }


        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("[SML]{1}",
         ErrorMessage = "O {0} deve ter apenas um caracter.")]
        [Display(Name = "Tamanho")]
        public string size { get; set; }

		/// <summary>
		/// Quantidade de um item numa ordem/compra
		/// </summary>
		[Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
		[Display(Name = "Quantidade")]
        [RegularExpression("^[1-9][0-9]*",
           ErrorMessage = "A {0} tem de ser maior do que um")]
        public int quantity { get; set; }

        /// <summary>
        /// Preço total de um determinado item numa ordem/compra (totalPrice = quantidade * preco/unid
        /// </summary>
        /// <summary>
        /// Preço unitário de um produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Preço")]
        public decimal totalPrice { get; set; }

        /// <summary>
        /// Campo auxiliar de introdução do Preço de um produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Preço")]
        [RegularExpression("[0-9]+[.,]?[0-9]{1,2}",
           ErrorMessage = "No {0} só pode usar algarismos, e se desejar," +
           " duas casas decimais no final.")]
        [NotMapped]
        public string totalPriceAux { get; set; }


        /// <summary>
        /// FK Order
        /// </summary>
        [ForeignKey(nameof(order))]
		public int orderFK { get; set; }
		public Order order { get; set; }
		
		/// <summary>
		/// FK produto
		/// </summary>
		[ForeignKey(nameof(product))]
		public int productFK { get; set; }
		public Product product { get; set; }
	}
}
