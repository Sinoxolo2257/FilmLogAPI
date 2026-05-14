using Xunit;
using Moq;
using FilmLogAPI.Services;
using FilmLogAPI.Models;
using FilmLogAPI.Controllers;
using FilmLogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace FilmLogAPI.Tests
{
    public class UnitTest1
    {
        private readonly Mock<IFilmRepository> _mockRepo;
        private readonly WatchlistController _controller;

        public UnitTest1()
        {
            _mockRepo = new Mock<IFilmRepository>();
            _controller = new WatchlistController(_mockRepo.Object);

            var claims = new List<Claim>
            {
                new Claim("userId", "1")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

        }

        [Fact]
        public async Task GetWatchList_ReturnsOkResult_WhenListIsNotEmpty()
        {
            // Arrange
            var userId = 1;
            var mockWatchList = new []
                {
                    new WatchList { WatchListId = 1, UserId = userId, Title = "Movie 1" , Year = 2021},
                    new WatchList { WatchListId = 2, UserId = userId, Title = "Movie 2", Year = 2022 }
                };

            _mockRepo.Setup(repo => repo.GetWatchListsByUserAsync(userId)).ReturnsAsync(mockWatchList);

            // Act
            var result = await _controller.GetWatchList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteFromWatchList_ReturnsNotFound_WhenTitleDoesNotExist()
        {
            // Arrange
            var userId = 1;
            var title = "NonExistentMovie";
            _mockRepo.Setup(repo => repo.DeleteFromWatchListAsync(userId, title)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteFromWatchList(title);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}