using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.UnitTests
{
	using System.Linq;

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
	}
}
