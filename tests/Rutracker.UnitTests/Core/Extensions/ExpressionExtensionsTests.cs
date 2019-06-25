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
        public void Expression_And_Should_Apply_And_Operator_Between_Two_Expression()
        {
            // Arrange
            const int expectedCount = 2;

            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 6;

            // Act
            var expression = left.And(right);
            var count = TestData().AsQueryable().Where(expression).Count();
            
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "Or() should apply || between the two expression")]
        public void Expression_Or_Should_Apply_Or_Operator_Between_Two_Expression()
        {
            // Arrange
            const int expectedCount = 4;

            Expression<Func<string, bool>> left = x => x.StartsWith('t');
            Expression<Func<string, bool>> right = x => x.Length == 1;

            // Act
            var expression = left.Or(right);
            var count = TestData().AsQueryable().Where(expression).Count();

            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "And(null) should return ArgumentNullException")]
        public void Expression_And_Null_Should_Return_ArgumentNullException()
        {
            // Arrange
            Expression<Func<string, bool>> left = x => string.IsNullOrEmpty(x);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => left.And(null));
        }

        [Fact(DisplayName = "Or(null) should return ArgumentNullException")]
        public void Expression_Or_Null_Should_Return_ArgumentNullException()
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