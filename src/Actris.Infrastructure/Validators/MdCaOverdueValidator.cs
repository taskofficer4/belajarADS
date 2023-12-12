using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class MdCaOverdueValidator : AbstractValidator<MdCaOverdueImpactDto>
    {
        private FormState _state;

        private IMdCaOverdueImpactService _svc;

        public MdCaOverdueValidator(FormState state, IMdCaOverdueImpactService svc)
        {
            _state = state;
            _svc = svc;

            RuleFor(x => x.OverdueImpact).NotEmpty();
            RuleFor(x => x.ImpactBahasa).NotEmpty();

            if (_state == FormState.Create)
            {
                RuleFor(x => x.OverdueImpact).Must(o => _svc.IsExist(o) == false).WithMessage(o=> $"Overdue Impact with name '{o.OverdueImpact}' is exists.");
            }
        }
    }
}
