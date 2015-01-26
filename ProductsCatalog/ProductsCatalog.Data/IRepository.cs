﻿using System.Linq;

namespace ProductsCatalog.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(int id);

        T GetById(string id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}