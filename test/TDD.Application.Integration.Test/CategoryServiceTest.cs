using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Application.Contracts;
using TDD.Domain;
using TDD.Domain.Infrastructure;
using TDD.Infrastructure.Repositories;

namespace TDD.Application.Integration.Test
{
    public class CategoryServiceTest : PersistantTest
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepository = new CategoryRepository(this._context);
            _categoryService = new CategoryService(_categoryRepository);
        }

        [Fact]
        public void Save_Category_In_Database()
        {
            //arrange
            var expected = new Category("Mobile", "001", 0);
            _categoryService.Create(expected);
            this.DetachedAllEntites();
            //act
            var actual = _categoryRepository.GetById(expected.Id);
            //assert
            expected.Should().BeEquivalentTo(actual, a => a.Excluding(z => z.Id));

        }
        [Fact]
        public void GetAll_RetrunEmptyList_WhenCategoryNotexist()
        {

            //act
            var actual = _categoryService.GetAll();

            //assert
            actual.Should().BeNullOrEmpty();

        }

        [Fact]
        public void GetAll_RetrunList_WhenSomeCategoryexist()
        {
            //arrange
            var category1 = new Category("Mobile", "001", 0);
            var category2 = new Category("Labtop", "002", 1);
            _categoryService.Create(category1);
            _categoryService.Create(category2);

            //act
            var actual = _categoryRepository.GetAll();

            //assert
            actual.Should().NotBeNullOrEmpty();
            actual.Should().HaveCount(2);
        }

        [Fact]
        public void GetById_RetrunNull_WhenCategoryNotExist()
        {
            //arrange
            var id = Guid.NewGuid();
            //act            
            var result = _categoryService.GetById(id);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetById_RetruCategory_WhenCategoryExist()
        {
            //arrange
            var existcategory = new Category("Mobile", "001", 1);
            _categoryService.Create(existcategory);
            //act
            var result = _categoryService.GetById(existcategory.Id);
            //assert
            result.Should().BeEquivalentTo(existcategory);

        }

        [Fact]
        public void AddCategory_WhenCreateSuccessfull()
        {
            //arrange
            var category = new Category("Mobile", "01", 1);
           
            //act
            var result = _categoryService.Create(category);

            //assert
            result.Should().BeGreaterThan(0);

        }

        [Fact]
        public void DeleteCategory_WhenCategoryNotExist()
        {
            //arrange
            var categoryId = Guid.NewGuid();

            //act
            var requestAction = () => _categoryService.DeleteById(categoryId);

            //assert

            requestAction.Should().Throw<Exception>().WithMessage("Category not exist");
        }

    }
}
