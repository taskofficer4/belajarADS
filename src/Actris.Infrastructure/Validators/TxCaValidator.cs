using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
	public class TxCaValidator : AbstractValidator<TxCaDto>
	{
		private FormState _state;

		public TxCaValidator(FormState state)
		{
			_state = state;

			RuleFor(x => x.Recomendation).NotEmpty();
			RuleFor(x => x.ResponsibleManager).NotEmpty().WithName("Responsible Manager");
			RuleFor(x => x.Pic1).NotEmpty().WithName("PIC 1");

			When(o => !string.IsNullOrEmpty(o.Pic1), () =>
			{
				RuleFor(x => x.Pic2).NotEqual(o => o.Pic1).WithMessage("Cannot same with PIC 1");
			});

			RuleFor(x => x.DueDate).NotEmpty();
			RuleFor(x => x.CorrectiveActionPriority).NotEmpty().WithName("Priority Level");
		}
	}
}
