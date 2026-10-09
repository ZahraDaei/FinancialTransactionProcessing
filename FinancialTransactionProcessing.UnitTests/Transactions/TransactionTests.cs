using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class TransactionTests
    {
        [Fact]
        public void PendingToProcessingTest()
        {
            //Arange
            var transaction = new Transaction("123456", "456789", 10000M, "IR");
            //Act
            transaction.MarkAsProcessing();
            //Assert
            Assert.Equal(transaction.Status, TransactionStatus.Processing);
        }
        [Fact]
        public void PendingToCompletedShouldThrow()
        {
            //Arange
            var transaction = new Transaction("123456", "456789", 10000M, "IR");
            //Act
            Action completeAction = () => transaction.MarkAsCompleted();
            //Assert
            Assert.Throws<InvalidOperationException>(completeAction);
        }
        
        [Fact]
        public void ProcessingToProcessingShouldThrow()
        {
            //Arange
            var transaction = new Transaction("123456", "456789", 10000M, "IR");
            //Act
            transaction.MarkAsProcessing();
            Action processingAction = () => transaction.MarkAsProcessing();
            //Assert
            Assert.Throws<InvalidOperationException>(processingAction);
        }

        [Fact]
        public void MarkAsComplete_WhenProcessing_ShouldChangeStatusToComplete()
        {
            //Arrange
            var transaction= new Transaction("123456", "456789", 10000M, "IR");

            //Act
            transaction.MarkAsProcessing();
            transaction.MarkAsCompleted();

            //Assert
            Assert.Equal(transaction.Status, TransactionStatus.Completed);
        }

        [Fact]
        public void MarkAsFailed_WhenProcessing_ShouldChangeStatusToFail()
        {
            //Arrange
            var transaction= new Transaction("123456", "456789", 10000M, "IR");

            //Act
            transaction.MarkAsProcessing();
            transaction.MarkAsFailed();

            //Assert
            Assert.Equal(transaction.Status, TransactionStatus.Failed);
        }

        [Fact]
        public void MarkAsProcessing_WhenComplete_ShouldThrow()
        {
            //Arrange
            var transaction= new Transaction("123456", "456789", 10000M, "IR");

            //Act
            transaction.MarkAsProcessing();
            transaction.MarkAsCompleted();

            //Assert
            Action processingAction=() => transaction.MarkAsProcessing();
            Assert.Throws<InvalidOperationException>(processingAction);
        }

        [Fact]
        public void MarkAsComplete_WhenPending_ShouldThrow()
        {
            //Arrange
            var transaction= new Transaction("123456", "456789", 10000M, "IR");

            //Act

            //Assert
            Action completeAction=() => transaction.MarkAsCompleted();
            Assert.Throws<InvalidOperationException>(completeAction);
        }
        [Fact]
        public void MarkAsProcessing_WhenFailed_ShouldThrow()
        {
            //Arrange
            var transaction= new Transaction("123456", "456789", 10000M, "IR");

            //Act
            transaction.MarkAsProcessing();
            transaction.MarkAsFailed();

            //Assert
            Action processingAction=() => transaction.MarkAsProcessing();
            Assert.Throws<InvalidOperationException>(processingAction);
        }

        [Fact]
        public void CreateTransaction_negativeAmount_shouldThrow()
        {
            //Arrange
            var amount = -12M;
            //Act
            Action negativeAmount = () => new Transaction("1234567891234", "4563217893214", amount, "IR");
            //Assert
            Assert.Throws<ArgumentException>(negativeAmount);
        
        }

        [Fact]
        public void CreateTransaction_zeroAmount_shouldThrow()
        {
            //Arrange
            var amount = 0;
            //Act
            Action negativeAmount = () => new Transaction("1234567891234", "4563217893214", amount, "IR");
            //Assert
            Assert.Throws<ArgumentException>(negativeAmount);
        
        }

    
    }
}
