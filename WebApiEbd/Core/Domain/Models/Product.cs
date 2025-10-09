using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEbd.Core.Domain.Models;

[Table("product")]
public partial class Product
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] [StringLength(200)] public string Name { get; set; } = null!;

    [Column("description")] public string? Description { get; set; }

    [Column("model")] [StringLength(100)] public string? Model { get; set; }

    [Column("brand_id")] public int BrandId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Product")]
    public virtual Brand Brand { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseOrderDevice> PurchaseOrderDevice { get; set; } = new List<PurchaseOrderDevice>();
}