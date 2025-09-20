
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[PrimaryKey("OrderId", "DeviceId")]
[Table("purchase_order_device")]
public partial class PurchaseOrderDevice
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Key]
    [Column("device_id")]
    public int DeviceId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }

    [ForeignKey("DeviceId")]
    [InverseProperty("PurchaseOrderDevice")]
    public virtual Device Device { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("PurchaseOrderDevice")]
    public virtual PurchaseOrder Order { get; set; } = null!;
}
