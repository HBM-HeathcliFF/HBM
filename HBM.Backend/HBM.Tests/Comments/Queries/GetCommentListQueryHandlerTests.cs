using AutoMapper;
using HBM.Application.Comments.Queries.GetCommentList;
using HBM.Application.Posts.Queries.GetPostList;
using HBM.Persistence;
using HBM.Tests.Common;
using Shouldly;

namespace HBM.Tests.Comments.Queries
{
    [Collection("QueryCollection")]
    public class GetCommentListQueryHandlerTests
    {
        private readonly HbmDbContext Context;
        private readonly IMapper Mapper;

        public GetCommentListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCommentListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCommentListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCommentListQuery
                {
                    PostId = HbmContextFactory.PostIdForUpdate
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CommentListVm>();
            result.Comments.Count.ShouldBe(2);
        }
    }
}