using ProductsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ProductsCatalog.Data
{
    public class UowData : IUowData
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new ApplicationDbContext())
        {
        }

        public UowData(DbContext context)
        {
            this._context = context;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this._repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this._repositories.Add(typeof(T), Activator.CreateInstance(type, this._context));
            }

            return (IRepository<T>)this._repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }
    }
}