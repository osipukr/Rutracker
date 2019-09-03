using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.UnitTests.Setup
{
    public static class MockInitializer
    {
        public static RutrackerContext GetRutrackerContext()
        {
            var options = new DbContextOptionsBuilder<RutrackerContext>().Options;
            var mockContext = new DbContextMock<RutrackerContext>(options);

            mockContext.CreateDbSetMock(x => x.Categories, DataInitializer.GetTestCategories());
            mockContext.CreateDbSetMock(x => x.Subcategories, DataInitializer.GetTestSubcategories());
            mockContext.CreateDbSetMock(x => x.Torrents, DataInitializer.GeTestTorrents());
            mockContext.CreateDbSetMock(x => x.Files, DataInitializer.GetTestFiles());
            mockContext.CreateDbSetMock(x => x.Comments, DataInitializer.GetTestComments());
            mockContext.CreateDbSetMock(x => x.Likes, DataInitializer.GetTestLikes());

            return mockContext.Object;
        }

        public static ICategoryRepository GetCategoryRepository()
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            var categories = new DbQueryMock<Category>(DataInitializer.GetTestCategories()).Object;

            repositoryMock.Setup(x => x.GetAll()).Returns(categories);

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => categories.SingleOrDefault(x => x.Id == id));

            return repositoryMock.Object;
        }

        public static ISubcategoryRepository GetSubcategoryRepository()
        {
            var repositoryMock = new Mock<ISubcategoryRepository>();
            var subcategories = new DbQueryMock<Subcategory>(DataInitializer.GetTestSubcategories()).Object;

            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Subcategory, bool>>>()))
                .Returns<Expression<Func<Subcategory, bool>>>(subcategories.Where);

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => subcategories.SingleOrDefault(x => x.Id == id));

            return repositoryMock.Object;
        }

        public static ITorrentRepository GetTorrentRepository()
        {
            var repositoryMock = new Mock<ITorrentRepository>();
            var torrents = new DbQueryMock<Torrent>(DataInitializer.GeTestTorrents()).Object;

            repositoryMock.Setup(x => x.GetAll()).Returns(torrents);

            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .Returns<Expression<Func<Torrent, bool>>>(torrents.Where);

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => torrents.SingleOrDefault(x => x.Id == id));

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .ReturnsAsync((Expression<Func<Torrent, bool>> expression) => torrents.SingleOrDefault(expression));

            repositoryMock.Setup(x => x.CountAsync()).ReturnsAsync(torrents.Count);

            repositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .ReturnsAsync((Expression<Func<Torrent, bool>> expression) => torrents.Count(expression));

            repositoryMock.Setup(x => x.ExistAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => torrents.Any(x => x.Id == id));

            repositoryMock.Setup(x => x.Search(null, null, null))
                .Returns<string, long?, long?>((search, sizeFrom, sizeTo) => torrents);

            repositoryMock.Setup(x => x.PopularTorrents(It.IsAny<int>()))
                .Returns<int>(x => torrents.Take(x));

            return repositoryMock.Object;
        }

        public static ICommentRepository GetCommentRepository()
        {
            var repositoryMock = new Mock<ICommentRepository>();
            var comments = new DbQueryMock<Comment>(DataInitializer.GetTestComments()).Object;

            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns<Expression<Func<Comment, bool>>>(comments.Where);

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Comment, bool>>>()))
                .ReturnsAsync((Expression<Func<Comment, bool>> expression) => comments.SingleOrDefault(expression));

            return repositoryMock.Object;
        }

        public static ILikeRepository GetLikeRepository()
        {
            var repositoryMock = new Mock<ILikeRepository>();
            var likes = new DbQueryMock<Like>(DataInitializer.GetTestLikes()).Object;

            repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Like, bool>>>()))
                .ReturnsAsync((Expression<Func<Like, bool>> expression) => likes.SingleOrDefault(expression));

            return repositoryMock.Object;
        }

        public static IUnitOfWork GetUnitOfWork()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

            return unitOfWorkMock.Object;
        }
    }
}