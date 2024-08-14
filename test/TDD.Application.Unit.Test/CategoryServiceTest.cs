using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TDD.Domain;
using TDD.Domain.Infrastructure;

namespace TDD.Application.Unit.Test
{
    public class CategoryServiceTest
    {
        private CategoryService _categoryService;
        private ICategoryRepository _categoryRepository = Substitute.For<ICategoryRepository>();

        public CategoryServiceTest()
        {
            _categoryService = new CategoryService(_categoryRepository);
        }
        [Fact]
        public void Save_Category_in_DataBase()
        {   
            //arrange
            var input = new Category("Mobile", "01", 1);
            //act
            _categoryService.Create(input);
            //assert
            _categoryRepository.Received(1);
        }

        [Fact]
        public void GetAll_RetrunEmptyList_WhenCategoryNotexist()
        {
            _categoryRepository.GetAll().Returns(Enumerable.Empty<Category>());

            var result= _categoryService.GetAll();
            result.Should().BeEmpty();           
            
        }

        [Fact]
        public void GetAll_RetrunList_WhenSomeCategoryexist()
        {
            var categories=new List<Category>() { 
            new Category("Mobile", "01", 1)

            };
            _categoryRepository.GetAll().Returns(categories);

            var result = _categoryService.GetAll();
            result.Should().NotBeEmpty();

        }

        [Fact]
        public void GetById_RetrunNull_WhenCategoryNotExist()
        {
           
            _categoryRepository.GetById(Arg.Any<Guid>()).ReturnsNull();

            var result = _categoryService.GetById(Arg.Any<Guid>());
            
            result.Should().BeNull();

        }

        [Fact]
        public void GetById_RetruCategory_WhenCategoryExist()
        {
            var existcategory = new Category("Mobile", "01", 1);
           _categoryRepository.GetById(existcategory.Id).Returns(existcategory);


            var result = _categoryService.GetById(existcategory.Id);
           
            result.Should().BeEquivalentTo(existcategory);

        }
        [Fact]
        public void AddCategory_WhenCreateSuccessfull()
        {
            //arrange
            var category = new Category("Mobile", "01", 1);
            _categoryRepository.Add(category).Returns(1);

            //act
            var result = _categoryService.Create(category);

           //assert
            result.Should().BeGreaterThan(0);

        }

        [Fact]
        public void DeleteCategory_WhenCategoryExist()
        {
            //arrange
            var existCategory = new Category("Mobile", "01", 1);
            _categoryRepository.GetById(existCategory.Id).Returns(existCategory);
            _categoryRepository.Delete(existCategory).Returns(1);

            //act
            var result = _categoryService.DeleteById(existCategory.Id);

            //assert
            result.Should().BeGreaterThan(0);

        }
        [Fact]
        public void DeleteCategory_WhenCategoryNotExist()
        {
            //arrange
            var category = new Category("Mobile", "01", 1);
                   
            //act
            var requestAction =()=> _categoryService.DeleteById(category.Id);

            //assert

            requestAction.Should().Throw<Exception>().WithMessage("Category not exist");
        }
    }
}