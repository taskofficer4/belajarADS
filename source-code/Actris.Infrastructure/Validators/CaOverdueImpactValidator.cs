using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class CaOverdueImpactValidator : AbstractValidator<CaOverdueImpactDto>
    {
        private FormState _state;

        public CaOverdueImpactValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.ImpactTitle).NotEmpty().WithName("Title");
            RuleFor(x => x.ImpactValue).NotEmpty().WithName("Value");

            RuleFor(x => x.ImpactTitle2).NotEmpty().WithName("Title");
            RuleFor(x => x.ImpactValue2).NotEmpty().WithName("Value");
        }
    }
}
