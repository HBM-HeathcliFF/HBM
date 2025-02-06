using AutoMapper;
using HBM.Application.Comments.Queries.GetCommentList;
using HBM.Application.Reactions.Queries.GetReactionList;
using HBM.Persistence;
using HBM.Tests.Common;
using Shouldly;

namespace HBM.Tests.Reactions.Queries
{
    [Collection("QueryCollection")]
    public class GetReactionListQueryHandlerTests
    {
        private readonly HbmDbContext Context;
        private readonly IMapper Mapper;

        public GetReactionListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetReactionListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetReactionListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetReactionListQuery
                {
                    PostId = HbmContextFactory.PostIdForDelete
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ReactionListVm>();
            result.Reactions.Count.ShouldBe(2);
        }
    }
}