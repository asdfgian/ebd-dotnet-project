using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[Table("device")]
[Index("SerialNumber", Name = "device_serial_number_key", IsUnique = true)]
public partial class Device
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }

    [Column("model")]
    [StringLength(100)]
    public string? Model { get; set; }

    [Column("serial_number")]
    [StringLength(100)]
    public string SerialNumber { get; set; } = null!;

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("brand_id")]
    public int BrandId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Device")]
    public virtual Brand Brand { get; set; } = null!;

    [InverseProperty("Device")]
    public virtual ICollection<ContractsDevice> ContractsDevice { get; set; } = [];

    [InverseProperty("Device")]
    public virtual ICollection<Movement> Movement { get; set; } = [];

    [InverseProperty("Device")]
    public virtual ICollection<PurchaseOrderDevice> PurchaseOrderDevice { get; set; } = [];
}
