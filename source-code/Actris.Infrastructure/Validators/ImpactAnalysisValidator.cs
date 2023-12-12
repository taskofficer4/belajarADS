using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class ImpactAnalysisValidator : AbstractValidator<ImpactAnalysisDto>
    {
        private FormState _state;

        public ImpactAnalysisValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.AnalysisTitle).NotEmpty().WithName("Title");
            RuleFor(x => x.AnalysisValue).NotEmpty().WithName("Value");
        }
    }
}
