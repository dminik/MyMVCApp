namespace WebAPI.UnitTests.Controllers
{
	using System.Linq;
	using DataLayer.Context;
	using DataLayer.Model.Entities;

	using Moq;
	using NUnit.Framework;
	using ServiceLayer;
	using WebAPI.Controllers;


	[TestFixture]
	public class BookControllerTest
	{
		Mock<IBookService> mockBookService;
		BookController controller;

		[SetUp]
		public void SetUpTest()
		{
			mockBookService = new Mock<IBookService>();
			this.mockBookService.Setup(x => x.GetAll()).Returns(TestDataProvider.GetBooks());
			controller = new BookController(mockBookService.Object);
		}

		[Test]
		public void Get()
		{
			// Act
			var result = controller.Get().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(TestDataProvider.GetBooks().Count, result.Count());
		}

		[Test]
		public void GetById()
		{
			// Act
			var result = controller.Get(1);

			// Assert			
			this.mockBookService.Verify(x => x.GetById(It.Is<int>(y => y == 1)), Times.Once);
		}

		//[Test]
		//public void Post()
		//{
		//	// Arrange
		//	var itemForEdit = TestDataProvider.GetBooks()[0];

		//	// Act
		//	controller.Post(itemForEdit);

		//	// Assert			
		//	this.mockBookService.Verify(x => x.Update(It.IsAny<Book>()), Times.Once);
		//}

		//[Test]
		//public void Put()
		//{
		//	// Arrange
		//	//var itemForPut = TestDataProvider.GetBooks()[0];

		//	//// Act
		//	//controller.Put(itemForPut);

		//	//// Assert			
		//	//this.mockBookService.Verify(x => x.Create(It.IsAny<Book>()), Times.Once);
		//}

		[Test]
		public void Delete()
		{
			// Act
			controller.Delete(5);

			// Assert
			this.mockBookService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
		}
	}
}
