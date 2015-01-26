using System.Collections.Generic;

namespace ProductsCatalog.Models
{
    public class Category
    {
        private ICollection<Product> products;

        public Category()
        {
            products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products 
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
            }
        }
    }
}
