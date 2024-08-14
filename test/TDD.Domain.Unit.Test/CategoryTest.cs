using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Domain.Unit.Test
{
    public class CategoryTest
    {

        [Fact]
        public void Construct_Category()
        {
            var category = new Category("Mobile", "01", 1);
            category.Name.Should().Be("Mobile");
            category.Code.Should().Be("01");

        }
    }
}
