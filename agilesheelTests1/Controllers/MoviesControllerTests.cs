using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using agilesheel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace agilesheel.Controllers.Tests
{
    [TestClass()]
    public class MoviesControllerTests
    {
        private readonly StoreDbContext _context;

        [TestMethod]
        public async Task GetMovieIndex()
        {
            // Arrange 
            var mock = new Mock<StoreDbContext>(_context);
            var controller = new MoviesController(mock.Object);

            // Act 
            var result = await controller.IndexAsync("", "") as ViewResult;

            // Assert 
            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task GetMovieIndexWithParameters()
        {
            // Arrange 
            var mock = new Mock<StoreDbContext>(_context);
            var controller = new MoviesController(mock.Object);

            // Act 
            // Get Movies by Genre and is3D
            var result = await controller.IndexAsync("Action", "true") as ViewResult;

            // Assert 
            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task GetMovieDetail()
        {
            // Arrange
            var mock = new Mock<StoreDbContext>(_context);
            var controller = new MoviesController(_context);

            // Act 
            var result = await controller.Details(1);
            var okResult = result as OkObjectResult;

            // Assert 
            var actualConfiguration = okResult.Value as Configuration;
            Assert.AreEqual("Details", actualConfiguration);
        }
    }
}