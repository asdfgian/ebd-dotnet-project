using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Domain.Models;

[Table("brand")]
[Index("Name", Name = "brand_name_key", IsUnique = true)]
public class Brand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("country_origin_id")]
    public int CountryOriginId { get; set; }

    [ForeignKey("CountryOriginId")]
    [InverseProperty("Brand")]
    public virtual CountryOrigin CountryOrigin { get; set; } = null!;

    [InverseProperty("Brand")]
    public virtual ICollection<Device> Device { get; set; } = new List<Device>();
}
