using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Domain.Models;

[Table("department")]
[Index("Name", Name = "department_name_key", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
