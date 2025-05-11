using System;
using CalculatorLib;
using Xunit;

namespace CalculatorTests
{
    public class ExpressionEvaluatorTests
    {
        private readonly ExpressionEvaluator _evaluator;

        public ExpressionEvaluatorTests()
        {
            _evaluator = new ExpressionEvaluator();
        }

        [Fact]
        public void Evaluate_SimpleAddition_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "2 + 3";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Evaluate_SimpleSubtraction_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "10 - 5";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Evaluate_SimpleMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "4 * 6";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(24, result);
        }

        [Fact]
        public void Evaluate_SimpleDivision_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "20 / 4";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Evaluate_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string expression = "10 / 0";
            
            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _evaluator.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ComplexExpressionWithPrecedence_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "2 + 3 * 4";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(14, result);
        }

        [Fact]
        public void Evaluate_ComplexExpressionWithParentheses_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "(2 + 3) * 4";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(20, result);
        }

        [Fact]
        public void Evaluate_ComplexExpressionWithMultipleOperations_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "10 + 5 * 2 - 8 / 4";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(18, result);
        }

        [Fact]
        public void Evaluate_ComplexExpressionWithNestedParentheses_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "((10 + 5) * 2 - 8) / 4";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(5.5, result);
        }

        [Fact]
        public void Evaluate_ExpressionWithNegativeNumbers_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "-5 + 10";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Evaluate_ExpressionWithDecimals_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "2.5 * 4.2";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(10.5, result, precision: 10);
        }

        [Fact]
        public void Evaluate_ExpressionWithoutSpaces_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "3+4*2";
            
            // Act
            double result = _evaluator.Evaluate(expression);
            
            // Assert
            Assert.Equal(11, result);
        }

        [Fact]
        public void Evaluate_EmptyExpression_ThrowsArgumentException()
        {
            // Arrange
            string expression = "";
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _evaluator.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_InvalidExpression_ThrowsArgumentException()
        {
            // Arrange
            string expression = "2 + * 3";
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _evaluator.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ExpressionWithMismatchedParentheses_ThrowsArgumentException()
        {
            // Arrange
            string expression = "(2 + 3 * 4";
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _evaluator.Evaluate(expression));
        }
    }
}
