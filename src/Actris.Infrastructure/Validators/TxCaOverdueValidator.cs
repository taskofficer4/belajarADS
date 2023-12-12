using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
   public class TxCaOverdueValidator : AbstractValidator<TxCaDto>
   {
      public TxCaOverdueValidator()
      {
         RuleFor(x => x.OverdueReason).NotEmpty().WithName("Reason");
         RuleFor(x => x.OverdueImpact).NotEmpty().WithName("Impact");
         RuleFor(x => x.OverdueMitigation).NotEmpty().WithName("Mitigation / Next Step");
      }
   }
}
