﻿using BridgeDogs.Controllers;
using BridgeDogs.Interfaces;
using BridgeDogs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BridgeDogs.Tests.Controller
{
    public class DogsControllerTests
    {
        [Fact]
        public async Task GetDogs_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var dogParameters = new DogParameters
            {
                Attribute = "name",
                OrderBy = "asc",
                PageNumber = 1,
                PageSize = 10
            };

            var validDogs = new List<Dog>
            {
                new Dog { Name = "Flex", Color = "black", TailLength = 21, Weight = 67 },
                new Dog { Name = "Lex", Color = "black", TailLength = 15, Weight = 30 }
            };

            mockRepository.Setup(repo => repo.GetAllDogsAsync(dogParameters))
                .ReturnsAsync(validDogs);
            var controller = new DogsController(mockRepository.Object);

            // Act
            var result = await controller.GetDogs(dogParameters);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Dog>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dogsList = Assert.IsType<List<Dog>>(okResult.Value);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.NotNull(dogsList);
            Assert.Equal(validDogs, dogsList);
        }

        [Fact]
        public async Task GetDogs_InvalidAttribute_ReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var controller = new DogsController(mockRepository.Object);
            var dogParameters = new DogParameters
            {
                Attribute = "invalid_attribute",
                OrderBy = "asc",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await controller.GetDogs(dogParameters);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Dog>>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal("Invalid attribute or order", badRequest.Value);
        }

        [Fact]
        public async Task GetDogs_InvalidOrder_ReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var controller = new DogsController(mockRepository.Object);
            var dogParameters = new DogParameters
            {
                Attribute = "name",
                OrderBy = "invalid_order",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await controller.GetDogs(dogParameters);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Dog>>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal("Invalid attribute or order", badRequest.Value);
        }

        [Fact]
        public async Task GetDogs_NegativePagination_ReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var controller = new DogsController(mockRepository.Object);
            var dogParameters = new DogParameters
            {
                Attribute = "name",
                OrderBy = "asc",
                PageNumber = -1,
                PageSize = -10
            };

            // Act
            var result = await controller.GetDogs(dogParameters);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Dog>>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal("Negative pagination", badRequest.Value);
        }

        [Fact]
        public async Task GetDogs_Pagination_ReturnsExpectedResults()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var dogParameters = new DogParameters
            {
                Attribute = "name",
                OrderBy = "asc",
                PageNumber = 3,
                PageSize = 3
            };

            // Must be sorted
            var dogs = new List<Dog>
            {
                new Dog { Name = "Amid", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Bash", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Bill", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Bond", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Carl", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Core", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "David", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Kernel", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "King", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Max", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Shab", Color = "black", TailLength = 13, Weight = 48 },
                new Dog { Name = "Test", Color = "black", TailLength = 13, Weight = 48 }
            };


            mockRepository.Setup(repo => repo.GetAllDogsAsync(dogParameters))
                .ReturnsAsync(dogs.Skip(6).Take(3).ToList());
            var controller = new DogsController(mockRepository.Object);

            // Act
            var result = await controller.GetDogs(dogParameters);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Dog>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dogsList = Assert.IsType<List<Dog>>(okResult.Value);

            Assert.NotNull(result);
            Assert.Equal(3, dogsList.Count);
            Assert.Equal("David", dogsList[0].Name);
            Assert.Equal("King", dogsList[^1].Name);
        }

        [Fact]
        public async Task PostDog_ValidDog_ReturnsCreatedResult()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var validDog = new Dog { Name = "Test", Color = "white", TailLength = 14, Weight = 52 };
            mockRepository.Setup(repo => repo.DogExistsAsync("Test"))
                .ReturnsAsync(false);
            mockRepository.Setup(repo => repo.CreateDogAsync(validDog))
                .ReturnsAsync(validDog);
            var controller = new DogsController(mockRepository.Object);

            // Act
            var result = await controller.PostDog(validDog);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Dog>>(result);
            var createdResult = Assert.IsType<CreatedResult>(actionResult.Result);
            var createdDog = Assert.IsType<Dog>(createdResult.Value);
            Assert.Equal("Test", createdDog.Name);
        }

        [Fact]
        public async Task PostDog_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var controller = new DogsController(mockRepository.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.PostDog(dog: null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Dog>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostDog_DogExists_ReturnsConflict()
        {
            // Arrange
            var mockRepository = new Mock<IDogRepository>();
            var existingDog = new Dog { Name = "Test", Color = "white", TailLength = 14, Weight = 52 };
            mockRepository.Setup(repo => repo.DogExistsAsync("Test"))
                .ReturnsAsync(true);
            var controller = new DogsController(mockRepository.Object);

            // Act
            var result = await controller.PostDog(existingDog);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Dog>>(result);
            var conflictResult = Assert.IsType<ConflictObjectResult>(actionResult.Result);
            Assert.Equal("Dog with the same name already exists in DB.", conflictResult.Value);
        }

        public static IEnumerable<object[]> InvalidDogData()
        {
            yield return new object[] { new Dog { Name = "Test", Color = "white", TailLength = -5, Weight = -67 } };
            yield return new object[] { new Dog { Name = "Test", Color = "white", TailLength = 5, Weight = -67 } };
            yield return new object[] { new Dog { Name = "Test", Color = "white", TailLength = -5, Weight = 67 } };
        }

        [Theory]
        [MemberData(nameof(InvalidDogData))]
        public async Task PostDog_NegativeValues_ReturnsBadRequest(Dog dog)
        {
            // Arrange
            var mockrepository = new Mock<IDogRepository>();
            var controller = new DogsController(mockrepository.Object);

            // Act
            var result = await controller.PostDog(dog);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Dog>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
    }
}
