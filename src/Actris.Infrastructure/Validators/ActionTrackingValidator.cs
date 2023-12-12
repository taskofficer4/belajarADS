using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using FluentValidation;

namespace Actris.Infrastructure.Validators
{
    public class ActionTrackingValidator : AbstractValidator<TxActDto>
    {
        private FormState _state;

        public ActionTrackingValidator(FormState state)
        {
            _state = state;

            RuleFor(x => x.ActionTrackingSourceKey).NotEmpty().WithName("ACT Source");
            RuleFor(x => x.FindingDesc).NotEmpty().WithName("Finding Description");
            RuleFor(x => x.IssueDate).NotEmpty().WithName("Issue Date");
            //RuleFor(x => x.TypeShuRegion).NotEmpty().WithName("Shu / Regional");
            RuleFor(x => x.DirectorateRegionalID).NotEmpty().WithName("Directorate / Regional");
            RuleFor(x => x.DivisiZonaID).NotEmpty().WithName("Divisi / Zona");
            RuleFor(x => x.CompanyCode).NotEmpty().WithName("Company");
            RuleFor(x => x.WilayahkerjaID).NotEmpty().WithName("Wilayah Kerja");
            RuleFor(x => x.LocationID).NotEmpty().WithName("Location");
            RuleFor(x => x.SubLocation).NotEmpty().WithName("Sub Location");
            //RuleFor(x => x.IssueDate).NotEmpty();
        }
    }
}
