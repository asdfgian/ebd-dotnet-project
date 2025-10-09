using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[Table("user")]
[Index("Email", Name = "user_email_key", IsUnique = true)]
[Index("Username", Name = "user_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(200)]
    public string Email { get; set; } = null!;

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("phone")]
    [StringLength(9)]
    public string? Phone { get; set; }

    [Column("status")]
    [MaxLength(1)]
    public char Status { get; set; }

    [Column("gender")]
    [MaxLength(1)]
    public char Gender { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Contract> Contract { get; set; } = new List<Contract>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("User")]
    public virtual Department? Department { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Movement> MovementCreatedByNavigation { get; set; } = new List<Movement>();

    [InverseProperty("UserDestination")]
    public virtual ICollection<Movement> MovementUserDestination { get; set; } = new List<Movement>();

    [InverseProperty("UserOrigin")]
    public virtual ICollection<Movement> MovementUserOrigin { get; set; } = new List<Movement>();

    [InverseProperty("User")]
    public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; } = new List<PurchaseOrder>();

    [ForeignKey("RoleId")]
    [InverseProperty("User")]
    public virtual Role Role { get; set; } = null!;
}
