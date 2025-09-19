using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Domain.Models;

[Table("country_origin")]
[Index("Name", Name = "country_origin_name_key", IsUnique = true)]
public partial class CountryOrigin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("CountryOrigin")]
    public virtual ICollection<Brand> Brand { get; set; } = new List<Brand>();
}
