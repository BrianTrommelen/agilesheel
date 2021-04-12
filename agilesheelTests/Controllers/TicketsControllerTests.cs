﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using System.Configuration;

namespace agilesheel.Controllers.Tests
{
    [TestClass()]
    public class TicketsControllerTests
    {
        private readonly StoreDbContext _context;
        private IStoreRepository _repo;

        [TestMethod]
        public async Task GetTickets()
        {
            // Arrange 
            var mock = new Mock<StoreDbContext>(_context);
            var controller = new TicketsController(mock.Object, _repo);

            // Act 
            var result = await controller.Index() as ViewResult;

            // Assert 
            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task GetTicketById()
        {
            // Arrange 
            var mock = new Mock<StoreDbContext>(_context);
            var controller = new TicketsController(mock.Object, _repo);

            // Act 
            var result = await controller.Details(2) as ViewResult;

            // Assert 
            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void PriceTest()
        {
            // Arrange
            var actual = 9.00;
            var movie = _context.Movies.Find(2);

            // Act
            if (movie.Length <= 120)
            {
                actual = actual - 1.50;
            }

            // Assert
            Assert.AreEqual(7.50, actual);
        }
    }
}