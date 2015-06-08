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

	class SomeCachedEntityService<T, TKeyType> : CachedEntityService<T, TKeyType>
		where T : Entity<TKeyType>		
	{
		public SomeCachedEntityService(ICacheService cacheService, IGenericRepository<T, TKeyType> repository, IUnitOfWork unitOfWork)
			: base(cacheService, repository, unitOfWork)
		{
		}
	}

	[TestFixture]
	class CachedEntityServiceTest
	{		
		Mock<IBookRepository> mockBookRepository;		
		Mock<IDataRepositories> mockDataRepositories;
		Mock<ICacheService> mockCacheService;

		IEntityService<Book, int> serviceUnderTest;

		[SetUp]
		public void SetUpTest()
		{						
			mockCacheService = new Mock<ICacheService>();
			mockBookRepository = new Mock<IBookRepository>();			
			this.mockBookRepository.Setup(x => x.GetAll()).Returns(TestDataProvider.GetBooks());

			mockDataRepositories = new Mock<IDataRepositories>();
			this.mockDataRepositories.Setup(x => x.Books).Returns(this.mockBookRepository.Object);

			this.serviceUnderTest = new SomeCachedEntityService<Book, int>(
				mockCacheService.Object,
				mockDataRepositories.Object.Books, 
				mockDataRepositories.Object);
		}
		
		[Test]
		public void GetAll_CallTwice_Success()
		{				
			// Проверяем, что первый раз берется из репозитория, а второй раз из кэша.

			// Act first			
			var results1 = this.serviceUnderTest.GetAll().ToList();

			// Assert first			
			this.mockBookRepository.Verify(x => x.GetAll(), Times.Once);

			this.mockCacheService.Verify(x => x.AddOrUpdate(It.Is<string>(y => y == "IsGetAllOccuredCacheKey"),
															It.IsAny<Object>(),
															It.IsAny<string>(),
															It.IsAny<Action<string, Object>>(),
															It.IsAny<Action<string, Object>>()),
															Times.Once);

			this.mockCacheService.Verify(x => x.AddOrUpdate(It.IsAny<string>(), 
															It.IsAny<Book>(), 
															It.IsAny<string>(), 
															It.IsAny<Action<string, Book>>(), 
															It.IsAny<Action<string, Book>>()), 
															Times.Exactly(results1.Count()));
			

			this.mockCacheService.Verify(x => x.GetByGroupKey<Book>(It.IsAny<string>()), Times.Never);

			Assert.IsNotNull(results1);
			Assert.AreEqual(TestDataProvider.GetBooks().Count, results1.Count());

			// делаем вид, что выставился флажок в кеше isGetAllOccuredCacheKey
			mockCacheService.Setup(x => x.Get<object>(
													It.Is<string>(y => y == "IsGetAllOccuredCacheKey"), 
													It.IsAny<string>()))
									.Returns(new Object());

			mockCacheService.Setup(x => x.GetByGroupKey<Book>(It.Is<string>(y => y == "BookEntity_"))).Returns(TestDataProvider.GetBooks());


			// Act second
			var results2 = this.serviceUnderTest.GetAll();
			
			// Assert 
			Assert.IsNotNull(results2);
			
			this.mockBookRepository.Verify(x => x.GetAll(), Times.Once);			
			this.mockCacheService.Verify(x => x.GetByGroupKey<Book>(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void GetById_Success()
		{
			// Проверяем, что первый раз берется из репозитория, а второй раз из кэша.
			
			// Arrange 
			var testBook = TestDataProvider.GetBooks().Single(x => x.Id == 1);
			this.mockBookRepository.Setup(x => x.GetByKey(It.IsAny<int>())).Returns(testBook);

			// Act first			
			var results1 = this.serviceUnderTest.GetById(1);

			// Assert first			
			this.mockCacheService.Verify(x => x.Get<Book>(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
			this.mockBookRepository.Verify(x => x.GetByKey(It.IsAny<int>()), Times.Once);
			
			this.mockCacheService.Verify(x => x.AddOrUpdate(It.IsAny<string>(),
															It.IsAny<Book>(),
															It.IsAny<string>(),
															It.IsAny<Action<string, Book>>(),
															It.IsAny<Action<string, Book>>()),
															Times.Once);

			Assert.IsNotNull(results1);
			
			// делаем вид, что закешировали объект
			mockCacheService.Setup(x => x.Get<Book>(
													It.IsAny<string>(),
													It.IsAny<string>()))
									.Returns(testBook);			

			// Act second
			var results2 = this.serviceUnderTest.GetById(1);

			// Assert 
			Assert.IsNotNull(results2);
			this.mockCacheService.Verify(x => x.Get<Book>(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
			this.mockBookRepository.Verify(x => x.GetByKey(It.IsAny<int>()), Times.Once);

			this.mockCacheService.Verify(x => x.AddOrUpdate(It.IsAny<string>(),
															It.IsAny<Book>(),
															It.IsAny<string>(),
															It.IsAny<Action<string, Book>>(),
															It.IsAny<Action<string, Book>>()),
															Times.Once);			
		}

		//TODO Дописать остльные тесты
	}
}
