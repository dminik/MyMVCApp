using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.UnitTests
{
	using System.Linq;
	using System.Linq.Expressions;

	using DataLayer.Context;
	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;
	using DataLayer.Repository.Repositories.Base;

	using Moq;

	using NUnit.Framework;

	[TestFixture]
	class BookServiceTest
	{
		private Mock<IBookRepository> mockBookRepository;
		private IBookService service;

		Mock<IDataRepositories> dataRepositories;

		[SetUp]
		public void SetUpTest()
		{			
			mockBookRepository = new Mock<IBookRepository>();			
			this.mockBookRepository.Setup(x => x.GetAll()).Returns(TestDataProvider.GetBooks());

			dataRepositories = new Mock<IDataRepositories>();
			this.dataRepositories.Setup(x => x.Books).Returns(this.mockBookRepository.Object);

			this.service = new BookService(dataRepositories.Object);
		}

		[Test]
		public void GetById_Success()
		{
			// Act			
			this.service.GetById(1);

			// Assert
			this.mockBookRepository.Verify(x => x.GetByKey(It.IsAny<int>()), Times.Once);					
		}

		[Test]
		public void GetAll_Success()
		{					
			// Act			
			var results = this.service.GetAll();

			// Assert
			this.mockBookRepository.Verify(x => x.GetAll(), Times.Once);
			Assert.IsNotNull(results);
			Assert.AreEqual(TestDataProvider.GetBooks().Count, results.Count());
		}

		[Test]
		public void Create_Success()
		{
			// Arrange						
			var newItem = TestDataProvider.GetBooks()[0];

			// Act			
			this.service.Create(newItem);

			// Assert
			this.mockBookRepository.Verify(x => x.Add(It.IsAny<BookEntity>()), Times.Once);
			this.dataRepositories.Verify(x => x.Save(), Times.Once);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_InvalidParams_ThrowException()
		{
			// Act			
			this.service.Create(null);
		}

		[Test]
		public void Update_Success()
		{
			// Arrange						
			var item = TestDataProvider.GetBooks()[0];

			// Act			
			this.service.Update(item);

			// Assert
			this.mockBookRepository.Verify(x => x.Edit(It.IsAny<BookEntity>()), Times.Once);
			this.dataRepositories.Verify(x => x.Save(), Times.Once);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Update_InvalidParams_ThrowException()
		{			
			// Act			
			this.service.Update(null);
		}

		[Test]
		public void Delete_Success()
		{
			// Arrange						
			var item = TestDataProvider.GetBooks()[0];

			// Act			
			this.service.Delete(item.Id);

			// Assert
			this.mockBookRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
			this.dataRepositories.Verify(x => x.Save(), Times.Once);
		}
	}
}
