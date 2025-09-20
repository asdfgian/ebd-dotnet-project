
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[Table("movement")]
[Index("Date", Name = "movement_date_idx")]
public partial class Movement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("type")]
    [StringLength(20)]
    public string Type { get; set; } = null!;

    [Column("device_id")]
    public int DeviceId { get; set; }

    [Column("user_origin_id")]
    public int? UserOriginId { get; set; }

    [Column("user_destination_id")]
    public int? UserDestinationId { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("MovementCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("DeviceId")]
    [InverseProperty("Movement")]
    public virtual Device Device { get; set; } = null!;

    [ForeignKey("UserDestinationId")]
    [InverseProperty("MovementUserDestination")]
    public virtual User? UserDestination { get; set; }

    [ForeignKey("UserOriginId")]
    [InverseProperty("MovementUserOrigin")]
    public virtual User? UserOrigin { get; set; }
}
