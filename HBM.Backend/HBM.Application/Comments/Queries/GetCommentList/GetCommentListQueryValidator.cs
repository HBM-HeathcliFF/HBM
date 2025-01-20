using FluentValidation;

namespace HBM.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryValidator : AbstractValidator<GetCommentListQuery>
    {
        public GetCommentListQueryValidator()
        {
            RuleFor(x => x.PostId).NotEqual(Guid.Empty);
        }
    }
}