using Microsoft.VisualStudio.TestTools.UnitTesting;
using agilesheel.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using agilesheel.Models;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace agilesheel.Controllers.Tests
{
    [TestClass()]
    public class MoviesControllerTests
    {
        private readonly StoreDbContext _context;

        [TestMethod]
        public async Task GetMovie()
        {
            var httpContext = new DefaultHttpContext();
            // Arrange 
            var mock = new Mock<IStoreRepository>();
            var controller = new TouchscreenTicketsController(_context, mock.Object);

            // Act 
            var result = await controller.Index() as ViewResult;

            // Assert 
            Assert.IsNotNull(result.ViewName);
        }
    }
}