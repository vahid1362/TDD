using System.ComponentModel.DataAnnotations.Schema;

namespace TDD.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public Category(string name, string code, int priority)
        {
            Id=Guid.NewGuid();  
            Name = name;
            Code = code;
            Priority = priority;

        }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public int Priority { get; private set; }
    }
}