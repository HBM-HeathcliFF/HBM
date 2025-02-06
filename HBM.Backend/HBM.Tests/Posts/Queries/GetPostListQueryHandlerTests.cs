using AutoMapper;
using HBM.Application.Posts.Queries.GetPostList;
using HBM.Persistence;
using HBM.Tests.Common;
using Shouldly;

namespace HBM.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    public class GetPostListQueryHandlerTests
    {
        private readonly HbmDbContext Context;
        private readonly IMapper Mapper;

        public GetPostListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetPostListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetPostListQuery(),
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PostListVm>();
            result.Posts.Count.ShouldBe(4);
        }
    }
}