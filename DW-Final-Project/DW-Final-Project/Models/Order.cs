﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
	/// <summary>
	/// Dados de uma Ordem, i.e, de uma compra feita por um cliente
	/// </summary>
	public class Order
	{
		public Order() {
			orderItemList = new HashSet<OrderItem>();
		}
		/// <summary>
		/// PK
		/// </summary>
		public int id { get; set; }

        /// <summary>
        /// Preço total da Ordem/Compra
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
        /// Percentagem  do IVA
        /// </summary>
        [Display(Name = "IVA")]
		[RegularExpression("^[1-9][0-9]+$",
		 ErrorMessage = "Tem de escrever um valor para o {0} válido")]
		public int IVA { get; set; } = 23;

		/// <summary>
		/// Data da Order
		/// </summary>
		[Display(Name ="Data")]
		public DateTime date { get; set; } = DateTime.Now;

		/// <summary>
		/// FK person
		/// </summary>
		[ForeignKey(nameof(Person))]
		public int personFK { get; set; }
		public Person Person { get; set; }

		/// <summary>
		/// Lista de itens de uma Ordem/Compra
		/// </summary>
		public ICollection<OrderItem> orderItemList { get; set; }
	}
}
