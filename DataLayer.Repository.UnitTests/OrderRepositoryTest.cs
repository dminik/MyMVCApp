using System.Linq;

using DataLayer.Context;
using DataLayer.Context.UnitTests.Data.Context;
using DataLayer.Repository.Repositories;
using NUnit.Framework;

namespace DataLayer.Repository.UnitTests
{
	using DataLayer.Model.Entities;

	[TestFixture]
	public class OrderRepositoryTest
	{		
		[Test]
		public void GetAll_Success()
		{
			// Arrange
			var databaseContext = new OrderContext(); 			
			var repoUnderTest = new OrderRepository(databaseContext.MainContext);

			//Act
			var result = repoUnderTest.GetAll().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(TestDataProvider.GetOrders().Count, result.Count());

			var expectedOrderPromoCode = TestDataProvider.GetOrders()[0].PromoCode;
			Assert.AreEqual(expectedOrderPromoCode, result[0].PromoCode);
		}

		[Test]
		public void Create_Success()
		{
			// Arrange
			var databaseContext = new EmptyContext();
			
			var repoUnderTest = new OrderRepository(databaseContext.MainContext);
			var newOrder = TestDataProvider.GetOrders()[0];
			Assert.AreEqual(0, repoUnderTest.GetAll().ToList().Count());

			//Act
			repoUnderTest.Add(newOrder);

			// Assert
			var result = repoUnderTest.GetAll().ToList();
			Assert.IsNotNull(result);
			Assert.AreEqual(1, repoUnderTest.GetAll().ToList().Count());		
		}
	}
}
