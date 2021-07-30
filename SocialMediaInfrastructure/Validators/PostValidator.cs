using FluentValidation;
using SocialMediaCore.DTOs;

namespace SocialMediaInfrastructure.Validators
{
   public class PostValidator : AbstractValidator<PostDto>
   {
      public PostValidator()
      {
          RuleFor(post => post.Description)
                .NotNull()
                .Length(5,500)
                .NotEmpty();
      }
   }
}