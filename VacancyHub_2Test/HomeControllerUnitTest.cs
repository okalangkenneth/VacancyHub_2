using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VacancyHub_2.Controllers;
using VacancyHub_2.Models;
using Xunit;

namespace VacancyHub_2Test
{
    public class HomeControllerUnitTest
    {
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        private readonly HomeController _controller;
        private readonly DefaultHttpContext _httpContext;


        public HomeControllerUnitTest()
        {
            // Mock the ILogger<HomeController>
            _loggerMock = new Mock<ILogger<HomeController>>();

            // Instantiate the HomeController with the mocked ILogger<HomeController>
            _controller = new HomeController(_loggerMock.Object);

            // Set up a default HttpContext
            _httpContext = new DefaultHttpContext();

            // Mock the HttpContext on the controller to avoid NullReferenceException
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public void TestIndexActionReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestPrivacyActionReturnsViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestErrorActionReturnsViewResultWithCorrectModel()
        {
            // Arrange
            // Simulate the TraceIdentifier (this would normally be populated by the framework)
            _httpContext.TraceIdentifier = "TestTraceIdentifier";

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal("TestTraceIdentifier", model.RequestId);
        }
    }
}





