
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEbd.Core.Domain.Models;

[PrimaryKey("ContractId", "DeviceId")]
[Table("contracts_device")]
public partial class ContractsDevice
{
    [Key]
    [Column("contract_id")]
    public int ContractId { get; set; }

    [Key]
    [Column("device_id")]
    public int DeviceId { get; set; }

    [Column("rental_price")]
    [Precision(12, 2)]
    public decimal RentalPrice { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractsDevice")]
    public virtual Contract Contract { get; set; } = null!;

    [ForeignKey("DeviceId")]
    [InverseProperty("ContractsDevice")]
    public virtual Device Device { get; set; } = null!;
}
