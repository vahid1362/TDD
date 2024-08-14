using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Domain.Infrastructure
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        
        IEnumerable<Category> GetAll();
        Category GetById(Guid id);
        int Delete (Category category);

        
    }
}
