using AutoMapper;
using HBM.Application.Posts.Queries.GetPostDetails;
using HBM.Persistence;
using HBM.Tests.Common;
using Shouldly;

namespace HBM.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    public class GetPostQueryHandlerTests
    {
        private readonly HbmDbContext Context;
        private readonly IMapper Mapper;

        public GetPostQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostQueryHandler_Success()
        {
            // Arrange
            var handler = new GetPostQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetPostQuery
                {
                    Id = Guid.Parse("84E2CD4A-3A52-42C0-B903-BCDD9B215114")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PostVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today.ToString());
        }
    }
}