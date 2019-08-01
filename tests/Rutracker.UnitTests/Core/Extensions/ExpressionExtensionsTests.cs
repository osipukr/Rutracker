using System;
using System.Linq;
using System.Linq.Expressions;
using Rutracker.Core.Extensions;
using Xunit;

namespace Rutracker.UnitTests.Core.Extensions
{
    public class ExpressionExtensionsTests
    {
        private static IQueryable<string> TestData() =>
            new[]
            {
                "test 1",
                "test 2",
                "3",
                "test 4 test",
                "apple"
            }.AsQueryable();

        [Fact(DisplayName = "And() with valid parameters should return applied expression")]
        public void And_ValidExpressions_ReturnsCombinedExpression()
        {
            // Arrange
            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 6;
            const int expectedCount = 2;

            // Act
            var expression = left.And(right);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal(expectedCount, TestData().Where(expression).Count());
        }

        [Fact(DisplayName = "Or() with valid parameters should return applied expression")]
        public void Or_ValidExpressions_ReturnsCombinedExpression()
        {
            // Arrange
            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 1;
            const int expectedCount = 4;

            // Act
            var expression = left.Or(right);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal(expectedCount, TestData().Where(expression).Count());
        }

        [Fact(DisplayName = "And() with an invalid parameters should throw ArgumentNullException")]
        public void And_NullExpressions_ThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ((Expression<Func<string, bool>>)null).And(null));
        }

        [Fact(DisplayName = "Or() with an invalid parameters should throw ArgumentNullException")]
        public void Or_NullExpressions_ThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ((Expression<Func<string, bool>>)null).Or(null));
        }
    }
}