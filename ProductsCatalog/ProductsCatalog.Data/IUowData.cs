using ProductsCatalog.Models;

namespace ProductsCatalog.Data
{
    public interface IUowData
    {
        IRepository<Product> Products { get; }

        IRepository<Category> Categories { get; }

        int SaveChanges();

        void Dispose();
    }
}