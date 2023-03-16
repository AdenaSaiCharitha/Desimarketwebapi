using System.ComponentModel.DataAnnotations;

namespace DesiMarket.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
    }
}
