using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Ports;

namespace Fiap.TasteEase.Domain.Models;

[Table("order", Schema = "taste_ease")]
public class OrderModel : EntityModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("description")]
    [MaxLength(512)]
    public string? Description { get; set; }

    [Column("status")]
    [MaxLength(128)]
    public OrderStatus Status { get; set; }

    [Column("client_id", Order = 0)]
    [ForeignKey("client")]
    public Guid ClientId { get; set; }
    
    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }
    
    public virtual ICollection<OrderFoodModel>? Foods { get; set; } = null!;
    public virtual ICollection<OrderPaymentModel>? Payments { get; set; } = null!;
    public virtual ClientModel Client { get; set; } = null!;
}
