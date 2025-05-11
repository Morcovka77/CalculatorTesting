using System;
using CalculatorLib;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        #region Addition Tests

        [Fact]
        public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = 5;
            double b = 7;
            
            // Act
            double result = _calculator.Add(a, b);
            
            // Assert
            Assert.Equal(12, result);
        }

        [Fact]
        public void Add_PositiveAndNegativeNumber_ReturnsCorrectSum()
        {
            // Arrange
            double a = 5;
            double b = -7;
            
            // Act
            double result = _calculator.Add(a, b);
            
            // Assert
            Assert.Equal(-2, result);
        }

        [Fact]
        public void Add_TwoNegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = -5;
            double b = -7;
            
            // Act
            double result = _calculator.Add(a, b);
            
            // Assert
            Assert.Equal(-12, result);
        }

        [Fact]
        public void Add_ZeroAndNumber_ReturnsNumber()
        {
            // Arrange
            double a = 0;
            double b = 7;
            
            // Act
            double result = _calculator.Add(a, b);
            
            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Add_DecimalNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = 1.5;
            double b = 2.7;
            
            // Act
            double result = _calculator.Add(a, b);
            
            // Assert
            Assert.Equal(4.2, result, precision: 10);
        }

        #endregion

        #region Subtraction Tests

        [Fact]
        public void Subtract_TwoPositiveNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            double a = 10;
            double b = 3;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Subtract_PositiveAndNegativeNumber_ReturnsCorrectDifference()
        {
            // Arrange
            double a = 5;
            double b = -7;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(12, result);
        }

        [Fact]
        public void Subtract_TwoNegativeNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            double a = -5;
            double b = -7;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Subtract_ZeroFromNumber_ReturnsNumber()
        {
            // Arrange
            double a = 7;
            double b = 0;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Subtract_NumberFromZero_ReturnsNegativeNumber()
        {
            // Arrange
            double a = 0;
            double b = 7;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(-7, result);
        }

        [Fact]
        public void Subtract_DecimalNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            double a = 5.7;
            double b = 2.3;
            
            // Act
            double result = _calculator.Subtract(a, b);
            
            // Assert
            Assert.Equal(3.4, result, precision: 10);
        }

        #endregion

        #region Multiplication Tests

        [Fact]
        public void Multiply_TwoPositiveNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            double a = 5;
            double b = 7;
            
            // Act
            double result = _calculator.Multiply(a, b);
            
            // Assert
            Assert.Equal(35, result);
        }

        [Fact]
        public void Multiply_PositiveAndNegativeNumber_ReturnsNegativeProduct()
        {
            // Arrange
            double a = 5;
            double b = -7;
            
            // Act
            double result = _calculator.Multiply(a, b);
            
            // Assert
            Assert.Equal(-35, result);
        }

        [Fact]
        public void Multiply_TwoNegativeNumbers_ReturnsPositiveProduct()
        {
            // Arrange
            double a = -5;
            double b = -7;
            
            // Act
            double result = _calculator.Multiply(a, b);
            
            // Assert
            Assert.Equal(35, result);
        }

        [Fact]
        public void Multiply_NumberByZero_ReturnsZero()
        {
            // Arrange
            double a = 5;
            double b = 0;
            
            // Act
            double result = _calculator.Multiply(a, b);
            
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Multiply_DecimalNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            double a = 2.5;
            double b = 3.2;
            
            // Act
            double result = _calculator.Multiply(a, b);
            
            // Assert
            Assert.Equal(8, result, precision: 10);
        }

        #endregion

        #region Division Tests

        [Fact]
        public void Divide_TwoPositiveNumbers_ReturnsCorrectQuotient()
        {
            // Arrange
            double a = 10;
            double b = 2;
            
            // Act
            double result = _calculator.Divide(a, b);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_PositiveAndNegativeNumber_ReturnsNegativeQuotient()
        {
            // Arrange
            double a = 10;
            double b = -2;
            
            // Act
            double result = _calculator.Divide(a, b);
            
            // Assert
            Assert.Equal(-5, result);
        }

        [Fact]
        public void Divide_TwoNegativeNumbers_ReturnsPositiveQuotient()
        {
            // Arrange
            double a = -10;
            double b = -2;
            
            // Act
            double result = _calculator.Divide(a, b);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ZeroByNumber_ReturnsZero()
        {
            // Arrange
            double a = 0;
            double b = 5;
            
            // Act
            double result = _calculator.Divide(a, b);
            
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Divide_NumberByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            double a = 10;
            double b = 0;
            
            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
        }

        [Fact]
        public void Divide_DecimalNumbers_ReturnsCorrectQuotient()
        {
            // Arrange
            double a = 5.5;
            double b = 2.2;
            
            // Act
            double result = _calculator.Divide(a, b);
            
            // Assert
            Assert.Equal(2.5, result, precision: 10);
        }

        #endregion
    }
}
