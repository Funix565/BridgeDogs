using BridgeDogs.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeDogs.Tests.Controller
{
    public class PingControllerTests
    {
        [Fact]
        public void Ping_ReturnsVersion()
        {
            // Arrange
            var controller = new PingController();

            // Act
            var result = controller.Ping();

            // Assert
            Assert.Equal("Dogshouseservice.Version1.0.1", result.Content);
        }
    }
}
