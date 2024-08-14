using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Domain;

namespace TDD.Application.Contracts
{
    public interface ICategoryService
    {
        int Create(Category category);
       
        IEnumerable<Category> GetAll();
        
        Category GetById(Guid id);

        int DeleteById(Guid id);    
    }
}
