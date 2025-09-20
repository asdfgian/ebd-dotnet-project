
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[Table("purchase_order")]
public partial class PurchaseOrder
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("total")]
    [Precision(12, 2)]
    public decimal Total { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("provider_id")]
    public int ProviderId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Contract> Contract { get; set; } = [];

    [ForeignKey("ProviderId")]
    [InverseProperty("PurchaseOrder")]
    public virtual Provider Provider { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<PurchaseOrderDevice> PurchaseOrderDevice { get; set; } = [];

    [ForeignKey("UserId")]
    [InverseProperty("PurchaseOrder")]
    public virtual User User { get; set; } = null!;
}
