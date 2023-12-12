using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class ActSourceValidator : AbstractValidator<ActSourceDto>
    {
        private FormState _state;

        public ActSourceValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.SourceTitle).NotEmpty().WithName("Title");
            RuleFor(x => x.SourceValue).NotEmpty().WithName("Value");

            RuleFor(x => x.SourceTitle2).NotEmpty().WithName("Title");
            RuleFor(x => x.SourceValue2).NotEmpty().WithName("Value");

            //RuleFor(x => x.DirectorateRegionalID).NotEmpty().WithName("Directorate Regional");
        }
    }
}
