using ProductsCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalog.Data.FluentApiConfiguration
{
    public class CategoriesConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoriesConfiguration()
        {
            HasKey(category => category.Id);

            Property(category => category.Name)
                 .HasColumnAnnotation("Index", new IndexAnnotation(
                     new IndexAttribute("AK_Category", 2) { IsUnique = true }))
                     .HasMaxLength(200)
                     .IsRequired();
        }
    }
}
