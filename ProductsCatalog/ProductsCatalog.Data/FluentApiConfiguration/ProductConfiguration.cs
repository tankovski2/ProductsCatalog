using ProductsCatalog.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ProductsCatalog.Data.FluentApiConfiguration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(product => product.Id);

            //ToDo make index for CategoryId
            HasRequired(product => product.Category)
                .WithMany(categegory => categegory.Products)
                .HasForeignKey(product => product.CategoryId);

            Property(product => product.Name)
                 .HasColumnAnnotation("Index", new IndexAnnotation(
                     new IndexAttribute("AK_Product", 2) { IsUnique = true }))
                     .HasMaxLength(200)
                     .IsRequired();

            Property(product => product.ImageName).HasMaxLength(200);
        }
    }
}
