﻿using System;
using System.Linq;

namespace Mvc_Repository.Models.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        void Create(Category category);

        void Update(Category category);

        void Delete(Category category);

        Category Get(int categoryId);

        IQueryable<Category> GetAll();

        void SaveChanges();
    }
}