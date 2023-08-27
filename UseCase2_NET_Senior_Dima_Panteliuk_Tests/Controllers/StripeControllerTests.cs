using Microsoft.AspNetCore.Mvc;
using Moq;
using Stripe;
using UseCase2_NET_Senior_Dima_Panteliuk.Controllers;
using Xunit;

namespace UseCase2_NET_Senior_Dima_Panteliuk_Tests.Controllers
{
    public class StripeControllerTests
    {
        [Fact]
        public async Task GetBalanceAsync_ReturnsOkResult()
        {
            // Arrange
            var balanceServiceMock = new Mock<BalanceService>();
            balanceServiceMock.Setup(service => service.GetAsync(It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Balance());

            var controller = new StripeController(balanceServiceMock.Object, null);

            // Act
            var result = await controller.GetBalanceAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBalanceTransactionsAsync_ReturnsOkResult()
        {
            // Arrange
            var balanceTransactionServiceMock = new Mock<BalanceTransactionService>();
            balanceTransactionServiceMock.Setup(service => service.ListAsync(
                    It.IsAny<BalanceTransactionListOptions>(),
                    It.IsAny<RequestOptions>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StripeList<BalanceTransaction>());

            var controller = new StripeController(null, balanceTransactionServiceMock.Object);

            // Act
            var result = await controller.GetBalanceTransactionsAsync(null, null);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBalanceAsync_HandleStripeException_ReturnsStatusCode500()
        {
            // Arrange
            var balanceServiceMock = new Mock<BalanceService>();
            balanceServiceMock.Setup(service => service.GetAsync(It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new StripeException("Stripe Exception Message"));

            var controller = new StripeController(balanceServiceMock.Object, null);

            // Act
            var result = await controller.GetBalanceAsync();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetBalanceTransactionsAsync_HandleStripeException_ReturnsStatusCode500()
        {
            // Arrange
            var balanceTransactionServiceMock = new Mock<BalanceTransactionService>();
            balanceTransactionServiceMock.Setup(service => service.ListAsync(
                    It.IsAny<BalanceTransactionListOptions>(),
                    It.IsAny<RequestOptions>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new StripeException("Stripe Exception Message"));

            var controller = new StripeController(null, balanceTransactionServiceMock.Object);

            // Act
            var result = await controller.GetBalanceTransactionsAsync(null, null);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
