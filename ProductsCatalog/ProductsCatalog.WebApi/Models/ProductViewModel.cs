using ProductsCatalog.Models;
using ProductsCatalog.WebApi.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ProductsCatalog.WebApi.Models
{
    public class ProductViewModel
    {
       // private static string domain = string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(256)]
        public string ImagePath { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Image { get; set; }

        public static Expression<Func<Product, ProductViewModel>> FromProduct
        {
            get
            {
                return product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImagePath = PathConstants.IMAGES_ABSOLUTE_WEB_PATH + product.ImageName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name
                };
            }
        }

        public Product FromViewModel()
        {
            Product product = new Product
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CategoryId = this.CategoryId,
                ImageName = string.IsNullOrWhiteSpace(this.ImagePath)?this.ImagePath :
                                    this.ImagePath.Replace(PathConstants.IMAGES_ABSOLUTE_WEB_PATH, string.Empty)
            };

            return product;
        }
    }
}