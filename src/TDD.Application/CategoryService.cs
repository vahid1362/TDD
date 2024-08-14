using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Application.Contracts;
using TDD.Domain;
using TDD.Domain.Infrastructure;

namespace TDD.Application
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository; 
        }
        public int Create(Category category)
        {
          return _categoryRepository.Add(category); 
        }
        public IEnumerable<Category> GetAll() { 
            return _categoryRepository.GetAll();
        }

        public Category GetById(Guid id) { 
        
         return  _categoryRepository.GetById(id);
        }

        public int DeleteById(Guid id)
        {
           var category= _categoryRepository.GetById(id);
            if (category == null)
                throw new Exception("Category not exist");
            return _categoryRepository.Delete(category);
        }


    }
}
