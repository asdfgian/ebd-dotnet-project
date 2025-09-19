using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Domain.Models;

[Table("provider")]
[Index("Ruc", Name = "provider_ruc_key", IsUnique = true)]
public partial class Provider
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ruc")]
    [StringLength(11)]
    public string Ruc { get; set; } = null!;

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("district")]
    [StringLength(100)]
    public string District { get; set; } = null!;

    [Column("province")]
    [StringLength(100)]
    public string Province { get; set; } = null!;

    [Column("department", TypeName = "character varying")]
    public string Department { get; set; } = null!;

    [Column("status")]
    [MaxLength(1)]
    public string Status { get; set; } = null!;

    [Column("email")]
    [StringLength(200)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [InverseProperty("Provider")]
    public virtual ICollection<Contract> Contract { get; set; } = new List<Contract>();

    [InverseProperty("Provider")]
    public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; } = new List<PurchaseOrder>();
}
