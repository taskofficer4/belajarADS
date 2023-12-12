using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
   public class TxCaNeedActionValidator : AbstractValidator<TxCaDto>
   {
      public TxCaNeedActionValidator()
      {
         RuleFor(x => x.CorrectiveActionID).NotEmpty();
         RuleFor(x => x.FollowUpPlan).NotEmpty().WithName("Plan of Correction Action");
         RuleFor(x => x.CompletionDate).NotEmpty().WithName("Date of Completion");
      }
   }
}
