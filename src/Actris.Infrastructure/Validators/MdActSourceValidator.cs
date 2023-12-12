using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class MdActSourceValidator : AbstractValidator<MdActSourceDto>
    {
        private FormState _state;

        public MdActSourceValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.Source).NotEmpty();
            RuleFor(x => x.SourceBahasa).NotEmpty().WithName("Source (Bahasa)");

            RuleFor(x => x.DirectorateID).NotEmpty().WithName("Directorate");
            RuleFor(x => x.DivisionID).NotEmpty().WithName("Division");
            RuleFor(x => x.SubDivisionID).NotEmpty().WithName("Sub Division");
            RuleFor(x => x.DepartmentID).NotEmpty().WithName("Department");

            //RuleFor(x => x.SourceTitle2).NotEmpty().WithName("Title");
            //RuleFor(x => x.SourceValue2).NotEmpty().WithName("Value");

            //RuleFor(x => x.DirectorateRegionalID).NotEmpty().WithName("Directorate Regional");
        }
    }
}
