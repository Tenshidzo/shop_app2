using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace shop_app.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Зв’язок із таблицею Product
        public Product Product { get; set; }

        // Додаткові властивості для замовлення
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
    }
}
