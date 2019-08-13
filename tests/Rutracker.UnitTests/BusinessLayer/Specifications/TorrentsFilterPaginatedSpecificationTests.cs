﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Server.BusinessLayer.Specifications;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Specifications
{
    public class TorrentsFilterPaginatedSpecificationTests
    {
        public static TheoryData<int, int, string, IEnumerable<string>, long?, long?> Data =>
            new TheoryData<int, int, string, IEnumerable<string>, long?, long?>
            {
                { 0, 0, null, null, null, null },
                { 10, 20, Guid.Empty.ToString(), null, null, null },
                { 20, 30, null, new [] { "20", "30" }, long.MinValue, long.MaxValue / 2 },
                { 30, 40, Guid.Empty.ToString(), Enumerable.Empty<string>(), long.MinValue, long.MaxValue }
            };

        [Theory(DisplayName = "TorrentsFilterPaginatedSpecification() with valid parameters should return valid specification")]
        [MemberData(nameof(Data))]
        public void TorrentsFilterPaginatedSpecification_ValidParameters_ReturnsValidSpecification(
            int skip, int take,
            string search,
            IEnumerable<string> titles,
            long? sizeFrom, long? sizeTo)
        {
            // Act
            var specification = new TorrentsFilterPaginatedSpecification(skip, take, search, titles, sizeFrom, sizeTo);

            // Assert
            Assert.NotNull(specification);
            Assert.NotNull(specification.Criteria);
            Assert.True(specification.IsPagingEnabled);
            Assert.Equal(skip, specification.Skip);
            Assert.Equal(take, specification.Take);
        }
    }
}