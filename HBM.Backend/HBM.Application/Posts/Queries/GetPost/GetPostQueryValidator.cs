using FluentValidation;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(post => post.Id).NotEqual(Guid.Empty);
        }
    }
}