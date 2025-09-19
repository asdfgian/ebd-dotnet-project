using Microsoft.EntityFrameworkCore;
using WebApiEbd.Domain.Models;

namespace WebApiEbd.Infrastructure.Persistence.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Brand> Brand { get; set; }

    public virtual DbSet<Contract> Contract { get; set; }

    public virtual DbSet<ContractsDevice> ContractsDevice { get; set; }

    public virtual DbSet<CountryOrigin> CountryOrigin { get; set; }

    public virtual DbSet<Department> Department { get; set; }

    public virtual DbSet<Device> Device { get; set; }

    public virtual DbSet<Movement> Movement { get; set; }

    public virtual DbSet<Provider> Provider { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }

    public virtual DbSet<PurchaseOrderDevice> PurchaseOrderDevice { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brand_pkey");

            entity.HasOne(d => d.CountryOrigin).WithMany(p => p.Brand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("brand_country_origin_id_fkey");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contract_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValueSql("'PLACED'::character varying");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Order).WithMany(p => p.Contract).HasConstraintName("contract_order_id_fkey");

            entity.HasOne(d => d.Provider).WithMany(p => p.Contract)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_provider_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Contract)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_user_id_fkey");
        });

        modelBuilder.Entity<ContractsDevice>(entity =>
        {
            entity.HasKey(e => new { e.ContractId, e.DeviceId }).HasName("contracts_device_pkey");

            entity.HasOne(d => d.Contract).WithMany(p => p.ContractsDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contracts_device_contract_id_fkey");

            entity.HasOne(d => d.Device).WithMany(p => p.ContractsDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contracts_device_device_id_fkey");
        });

        modelBuilder.Entity<CountryOrigin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_origin_pkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("department_pkey");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("device_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValueSql("'ACTIVE'::character varying");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Brand).WithMany(p => p.Device)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("device_brand_id_fkey");
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movement_pkey");

            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MovementCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movement_created_by_fkey");

            entity.HasOne(d => d.Device).WithMany(p => p.Movement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movement_device_id_fkey");

            entity.HasOne(d => d.UserDestination).WithMany(p => p.MovementUserDestination).HasConstraintName("movement_user_destination_id_fkey");

            entity.HasOne(d => d.UserOrigin).WithMany(p => p.MovementUserOrigin).HasConstraintName("movement_user_origin_id_fkey");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("provider_pkey");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_order_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValueSql("'DRAFT'::character varying");

            entity.HasOne(d => d.Provider).WithMany(p => p.PurchaseOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_order_provider_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PurchaseOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_order_user_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrderDevice>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.DeviceId }).HasName("purchase_order_device_pkey");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Device).WithMany(p => p.PurchaseOrderDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_order_device_device_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.PurchaseOrderDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_order_device_order_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValueSql("'A'::bpchar");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Department).WithMany(p => p.User).HasConstraintName("user_department_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
