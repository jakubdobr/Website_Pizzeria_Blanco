using Api.Controllers;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _controller = new AuthController();
        }

        [Fact]

        public void Login_ValidUser_ReturnsToken()
        {
            var user = new LoginModel
            {
                Username = "admin",
                Password = "password"
            };

            var result = _controller.Login(user) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var tokenResponse = result.Value as TokenResponse;
            Assert.NotNull(tokenResponse);
            Assert.False(string.IsNullOrEmpty(tokenResponse.Token));
        }

        [Fact]

        public void Login_InvalidUser_ReturnsUnauthorized()
        {
            var user = new LoginModel
            {
                Username = "admin",
                Password = "wrongpassword"
            };

            var result = _controller.Login(user);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]

        public void Login_NullUser_ReturnsBadRequest()
        {
            var result = _controller.Login(null);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Invalid client request", badRequestResult.Value);

        }











    }
}
