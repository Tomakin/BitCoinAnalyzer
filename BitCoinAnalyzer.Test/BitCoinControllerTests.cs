using BitCoinAnalyzer.API.Controllers;
using BitCoinAnalyzer.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Test
{
    public class BitCoinControllerTests
    {
        [Fact]
        public void GetBitCoinDaily_ValidData_ReturnsOkResult()
        {
            // Arrange
            var bitcoinServiceMock = new Mock<IBitCoinService>();
            var controller = new BitCoinController(bitcoinServiceMock.Object);
            bitcoinServiceMock.Setup(service => service.GetBitCoinDaily()).Returns(new List<BitCoinChartModel>());

            // Act
            var result = controller.GetBitCoinDaily();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        // Similar test methods for GetBitCoinWeekly and GetBitCoinMonthly can be implemented
    }
}
