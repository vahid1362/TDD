using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Domain;
using TDD.Domain.Infrastructure;

namespace TDD.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CategoryRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public int Add(Category category)
        {
               _applicationContext.Categories.Add(category);
          return   _applicationContext.SaveChanges();
        }

        public int Delete(Category category)
        {
            _applicationContext.Categories.Entry(category).State=EntityState.Deleted;
            return _applicationContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _applicationContext.Categories;
        }

        public Category GetById(Guid id)
        {
            return _applicationContext.Categories.FirstOrDefault(x => x.Id == id);
        }
    }
}
