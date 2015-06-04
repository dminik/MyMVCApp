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

	using ServiceLayer.Cache;
	using ServiceLayer.Common;
	using ServiceLayer.Services;

	class SomeEntityService<T, TKeyType> : EntityService<T, TKeyType>
		where T : Entity<TKeyType>		
	{
		public SomeEntityService(IGenericRepository<T, TKeyType> repository, IUnitOfWork unitOfWork)
			: base(repository, unitOfWork)
		{
		}
	}

	[TestFixture]
	class EntityServiceTest
	{		
		Mock<IBookRepository> mockBookRepository;		
		Mock<IDataRepositories> mockDataRepositories;

		IEntityService<BookEntity, int> serviceUnderTest;

		[SetUp]
		public void SetUpTest()
		{						
			mockBookRepository = new Mock<IBookRepository>();			
			this.mockBookRepository.Setup(x => x.GetAll()).Returns(TestDataProvider.GetBooks());

			mockDataRepositories = new Mock<IDataRepositories>();
			this.mockDataRepositories.Setup(x => x.Books).Returns(this.mockBookRepository.Object);

			this.serviceUnderTest = new SomeEntityService<BookEntity, int>(mockDataRepositories.Object.Books, mockDataRepositories.Object);
		}

		[Test]
		public void GetById_Success()
		{
			// Act			
			this.serviceUnderTest.GetById(1);

			// Assert
			this.mockBookRepository.Verify(x => x.GetByKey(It.IsAny<int>()), Times.Once);					
		}

		[Test]
		public void GetAll_Success()
		{					
			// Act			
			var results = this.serviceUnderTest.GetAll();

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
			this.serviceUnderTest.Create(newItem);

			// Assert
			this.mockBookRepository.Verify(x => x.Add(It.IsAny<BookEntity>()), Times.Once);
			this.mockDataRepositories.Verify(x => x.Save(), Times.Once);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_InvalidParams_ThrowException()
		{
			// Act			
			this.serviceUnderTest.Create(null);
		}

		[Test]
		public void Update_Success()
		{
			// Arrange						
			var item = TestDataProvider.GetBooks()[0];

			// Act			
			this.serviceUnderTest.Update(item);

			// Assert
			this.mockBookRepository.Verify(x => x.Edit(It.IsAny<BookEntity>()), Times.Once);
			this.mockDataRepositories.Verify(x => x.Save(), Times.Once);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Update_InvalidParams_ThrowException()
		{			
			// Act			
			this.serviceUnderTest.Update(null);
		}

		[Test]
		public void Delete_Success()
		{
			// Arrange						
			var item = TestDataProvider.GetBooks()[0];

			// Act			
			this.serviceUnderTest.Delete(item.Id);

			// Assert
			this.mockBookRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
			this.mockDataRepositories.Verify(x => x.Save(), Times.Once);
		}
	}
}
