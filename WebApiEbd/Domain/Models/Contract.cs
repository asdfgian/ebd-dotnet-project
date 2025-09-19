using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Domain.Models;

[Table("contract")]
[Index("EndDate", Name = "contract_end_date_idx")]
public partial class Contract
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("amount")]
    [Precision(12, 2)]
    public decimal Amount { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("route")]
    public string? Route { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("provider_id")]
    public int ProviderId { get; set; }

    [Column("order_id")]
    public int? OrderId { get; set; }

    [InverseProperty("Contract")]
    public virtual ICollection<ContractsDevice> ContractsDevice { get; set; } = new List<ContractsDevice>();

    [ForeignKey("OrderId")]
    [InverseProperty("Contract")]
    public virtual PurchaseOrder? Order { get; set; }

    [ForeignKey("ProviderId")]
    [InverseProperty("Contract")]
    public virtual Provider Provider { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Contract")]
    public virtual User User { get; set; } = null!;
}
