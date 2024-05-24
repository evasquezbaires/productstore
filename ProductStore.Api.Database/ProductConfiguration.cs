using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Api.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Repository
{
    /// <summary>
    /// Configuration for the Product entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(key => key.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.UpdatedDate);
            builder.Property(p => p.Name).IsRequired();
            builder.HasNoDiscriminator();
        }
    }
}
