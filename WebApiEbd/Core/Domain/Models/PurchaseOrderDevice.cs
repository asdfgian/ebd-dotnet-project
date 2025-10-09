using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[PrimaryKey("OrderId", "ProductId")]
[Table("purchase_order_device")]
public partial class PurchaseOrderDevice
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("PurchaseOrderDevice")]
    public virtual PurchaseOrder Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("PurchaseOrderDevice")]
    public virtual Product Product { get; set; } = null!;
}
