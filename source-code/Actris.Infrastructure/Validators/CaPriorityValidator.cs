using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class CaPriorityValidator : AbstractValidator<CaPriorityDto>
    {
        private FormState _state;

        public CaPriorityValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.PriorityTitle).NotEmpty().WithName("Title");
            RuleFor(x => x.PriorityValue).NotEmpty().WithName("Value");

            RuleFor(x => x.PriorityTitle2).NotEmpty().WithName("Title");
            RuleFor(x => x.PriorityValue2).NotEmpty().WithName("Value");
        }
    }
}
