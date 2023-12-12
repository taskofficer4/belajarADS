using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class MdCaPriorityValidator : AbstractValidator<MdCaPriorityDto>
    {
        private FormState _state;

        private IMdCaPriorityService _svc;

        public MdCaPriorityValidator(FormState state, IMdCaPriorityService svc)
        {
            _state = state;
            _svc = svc;


            RuleFor(x => x.Priority).NotEmpty();
            RuleFor(x => x.PriorityBahasa).NotEmpty();

            if (_state == FormState.Create)
            {
                RuleFor(x => x.Priority).Must(o => _svc.IsPriorityExist(o) == false).WithMessage(o=> $"Priority with name '{o.Priority}' is exists.");
            }
        }
    }
}
