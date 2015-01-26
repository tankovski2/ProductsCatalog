using ProductsCatalog.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ProductsCatalog.WebApi.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
            }
        }

        public Category FromViewModel()
        {
            Category category = new Category
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };

            return category;
        }
    }
}