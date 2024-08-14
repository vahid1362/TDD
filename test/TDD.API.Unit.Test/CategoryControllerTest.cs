using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Application.Contracts;
using TDD.Domain;
using TDD_Rest.Controllers;

namespace TDD.API.Unit.Test
{
    public  class CategoryControllerTest
    {
        private readonly CategoryController _controller;
        private readonly ICategoryService _categoryService=NSubstitute.Substitute.For<ICategoryService>();
        public CategoryControllerTest()
        {
            _controller=new CategoryController(_categoryService);
        }
        [Fact]
        public void GetById_ReturnOkAndObject_WhenCategoryExist()
        {
            //arrange 
            var category= new Category("Mobile", "01", 1);
            _categoryService.GetById(category.Id).Returns(category);
            //act
           var result=(OkObjectResult)_controller.GetById(category.Id);
            //asset
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(category);

        }
        [Fact]
        public void GetById_ReturnNotFound_WhenCategoryNotExist()
        {
            //arrange 
            _categoryService.GetById(Arg.Any<Guid>()).ReturnsNull();
            //act
            var result = (NotFoundResult)_controller.GetById(Guid.NewGuid());
            //asset
            result.StatusCode.Should().Be(404);
           

        }
        [Fact]
        public void GetAll_ReturnEmpty_WhenCategoryNotExist()
        {
            //arrange 
            _categoryService.GetAll().Returns(Enumerable.Empty<Category>());
            //act
            var result = (OkObjectResult)_controller.GetAll();
            //asset
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<Category>>().Should().BeEmpty();


        }
        [Fact]
        public void GetAll_ReturnList_WhenCategoryExist()
        {  

            var Categories=new List<Category>() { 
            new Category("mobile","001",0),
            new Category("labtop","002",1)            
            };
            _categoryService.GetAll().Returns(Categories);
            //act
            var result = (OkObjectResult)_controller.GetAll();
            //asset
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<Category>>().Should().NotBeEmpty();


        }

        [Fact]
        public void Create_Category_WhenCategoryRequestIsvalid()
        {
            //arrange
            var catgory=new Category("mobile", "001", 0);
            _categoryService.Create(catgory).Returns(1);

            //act
            var result= (CreatedAtActionResult)_controller.Create(catgory);
            //arrange
            result.StatusCode.Should().Be(201);
            result.Value.As<Category>().Should().BeEquivalentTo(catgory);
            result.RouteValues!["id"].Should().BeEquivalentTo(catgory.Id);

        }


        [Fact]
        public void Create_Category_WhenCategoryRequestIsNotvalid()
        {
            //arrange
            var catgory = new Category("mobile", "001", 0);
            _categoryService.Create(catgory).Returns(0);

            //act
            var result = (BadRequestResult)_controller.Create(catgory);
            //arrange
            result.StatusCode.Should().Be(400);            
        }

        [Fact]
        public void DeleteById_ReturnOk_WhenCategoryIsdeleted()
        {
            //arrange
            var id=Guid.NewGuid();  
            _categoryService.DeleteById(id).Returns(1);

            //act
            var result = (OkResult)_controller.DeleteById(id);
            //arrange
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void DeleteById_ReturnNotFound_WhenCategoryIsNotdeleted()
        {
            //arrange
            var id = Guid.NewGuid();
            _categoryService.DeleteById(id).Returns(0);

            //act
            var result = (NotFoundResult)_controller.DeleteById(id);
            //arrange
            result.StatusCode.Should().Be(404);
        }
    }
}
