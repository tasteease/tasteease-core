using Fiap.TasteEase.Domain.Ports;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.TasteEase.Domain.Models
{
    [Table("order_payment", Schema = "taste_ease")]
    public class OrderPaymentModel : EntityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("order_id", Order = 0)]
        [ForeignKey("order")]
        public Guid OrderId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }
        
        [Column("paid")]
        public bool Paid { get; set; }
        
        [Column("reference")]
        [MaxLength(256)]
        public string Reference { get; set; }
        
        [Column("payment_link")]
        [MaxLength(4098)]
        public string PaymentLink { get; set; }

        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
        
        [Column("paid_date", TypeName = "timestamp without time zone")]
        public DateTime? PaidDate { get; set; }
        
        public virtual OrderModel Order { get; set; } = null!;
    }
}