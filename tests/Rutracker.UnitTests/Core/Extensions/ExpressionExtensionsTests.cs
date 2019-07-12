using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rutracker.Core.Extensions;
using Xunit;

namespace Rutracker.UnitTests.Core.Extensions
{
    public class ExpressionExtensionsTests
    {
        [Fact(DisplayName = "And() should apply && between the two expression")]
        public void And_TwoValidExpressions_ReturnsCombinedExpression()
        {
            // Arrange
            const int expectedCount = 2;
            var data = TestData().AsQueryable();

            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 6;

            // Act
            var expression = left.And(right);
            
            // Assert
            Assert.NotNull(expression);
            Assert.Equal(expectedCount, data.Where(expression).Count());
        }

        [Fact(DisplayName = "Or() should apply || between the two expression")]
        public void Or_TwoValidExpressions_ReturnsCombinedExpression()
        {
            // Arrange
            const int expectedCount = 4;
            var data = TestData().AsQueryable();

            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 1;

            // Act
            var expression = left.Or(right);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal(expectedCount, data.Where(expression).Count());
        }

        [Fact(DisplayName = "And() should return ArgumentNullException")]
        public void And_NullExpression_ReturnsArgumentNullException()
        {
            // Arrange
            Expression<Func<string, bool>> left = x => string.IsNullOrEmpty(x);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => left.And(null));
        }

        [Fact(DisplayName = "Or() should return ArgumentNullException")]
        public void Or_NullExpression_ReturnsArgumentNullException()
        {
            // Arrange
            Expression<Func<string, bool>> left = x => string.IsNullOrEmpty(x);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => left.Or(null));
        }

        private static IEnumerable<string> TestData() =>
            new[]
            {
                "test 1",
                "test 2",
                "3",
                "test 4 test",
                "apple"
            };
    }
}