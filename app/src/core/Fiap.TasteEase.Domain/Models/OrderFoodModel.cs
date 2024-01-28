using Fiap.TasteEase.Domain.Ports;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.TasteEase.Domain.Models
{
    [Table("order_food", Schema = "taste_ease")]
    public class OrderFoodModel : EntityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("order_id", Order = 0)]
        [ForeignKey("order")]
        public Guid OrderId { get; set; }

        [Column("food_id", Order = 1)]
        [ForeignKey("food")]
        public Guid FoodId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
        
        public virtual OrderModel Order { get; set; } = null!;
        public virtual FoodModel Food { get; set; } = null!;
    }
}