using FluentValidation;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class GetPostDetailsQueryValidator : AbstractValidator<GetPostDetailsQuery>
    {
        public GetPostDetailsQueryValidator()
        {
            RuleFor(post => post.Id).NotEqual(Guid.Empty);
        }
    }
}