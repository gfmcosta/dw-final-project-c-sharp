namespace DW_Final_Project.Models {

    /// <summary>
    /// Season de um produto
    /// </summary>
    public class Product_Season {
        public Product_Season() {
            productList = new HashSet<Product>();
        }
        /// <summary>
        /// PK
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nome da Season
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Lista de produtos com determinada Season
        /// </summary>

        public ICollection<Product> productList { get; set; }

    }
}
