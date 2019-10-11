using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class FileRepositoryTests
    {
        private readonly IFileRepository _fileRepository;
        private const int FilesCount = DataInitializer.FilesCount;

        public FileRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _fileRepository = new FileRepository(context);
        }

        [Fact]
        public void GetAll_10_ReturnsValidFiles()
        {
            // Arrange
            const int expectedCount = FilesCount;

            // Act
            var files = _fileRepository.GetAll();

            // Assert
            Assert.NotNull(files);
            Assert.Equal(expectedCount, files.Count());
        }

        [Fact]
        public void GetAll_10_ReturnsValidFilesByExpression()
        {
            // Arrange
            const int expectedId = 1;

            // Act
            var files = _fileRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(files);
            Assert.Single(files);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidFile()
        {
            // Arrange
            const int expectedId = FilesCount;

            // Act
            var file = await _fileRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(file);
            Assert.NotNull(file.Name);
            Assert.NotNull(file.Type);
            Assert.NotNull(file.Url);
            Assert.InRange(file.Size, 0, long.MaxValue);
            Assert.InRange(file.TorrentId, 1, int.MaxValue);
            Assert.Equal(expectedId, file.Id);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidFileByExpression()
        {
            // Arrange
            const int expectedId = FilesCount;

            // Act
            var file = await _fileRepository.GetAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(file);
            Assert.NotNull(file.Name);
            Assert.NotNull(file.Type);
            Assert.NotNull(file.Url);
            Assert.InRange(file.Size, 0, long.MaxValue);
            Assert.InRange(file.TorrentId, 1, int.MaxValue);
            Assert.Equal(expectedId, file.Id);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = FilesCount;

            // Act
            var count = await _fileRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = FilesCount;
            const int expectedCount = 1;

            // Act
            var count = await _fileRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = FilesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _fileRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = FilesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _fileRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}