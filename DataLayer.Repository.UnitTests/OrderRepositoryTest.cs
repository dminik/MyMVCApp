using System.Linq;

using DataLayer.Context;
using DataLayer.Context.UnitTests.Data.Context;
using DataLayer.Repository.Repositories;
using NUnit.Framework;

namespace DataLayer.Repository.UnitTests
{	
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
	}
}
