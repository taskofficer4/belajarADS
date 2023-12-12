using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
   public class TxCaProposeDueDateValidator : AbstractValidator<TxCaDto>
   {
      private FormState _state;

      public TxCaProposeDueDateValidator(FormState state)
      {
         _state = state;

         RuleFor(x => x.CorrectiveActionID).NotEmpty();
         RuleFor(x => x.ProposedDueDate).NotEmpty().WithName("Due Date Revision");
         RuleFor(x => x.ProposedDueDateData).NotEmpty().WithName("Justification");
      }
   }
}
