using BasicMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace registration.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Suppliers : Address
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Supplier);

            // 1 : N => Suppliers : Products
            builder.HasMany(s => s.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);
            builder.ToTable("Suppliers");
        }
    }
}
