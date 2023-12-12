using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Validators;
using Actris.Web.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Actris.Web.Areas.MasterData.Controllers
{
    public class CaOverdueImpactController : BaseCrudController<CaOverdueImpactDto, CaOverdueImpactDto>
    {
        public CaOverdueImpactController(ICaOverdueImpactService crudSvc) : base(crudSvc)
        {
        }

        public override async Task<ActionResult> Create(CaOverdueImpactDto model)
        {
            model.CreatedDate = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            return await BaseCreate(model, new CaOverdueImpactValidator(FormState.Create));
        }

        public override async Task<ActionResult> Edit(CaOverdueImpactDto model)
        {
            return await BaseUpdate(model, new CaOverdueImpactValidator(FormState.Edit));
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            return new FormDefinition
            {
                Title = "Overdue Impact",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                   new FieldSection
                    {
                        SectionName = "",
                        Fields = {
                                new Field {
                                Id = nameof(CaOverdueImpactDto.CorrectiveActionOverdueImpactID),
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(CaOverdueImpactDto.ImpactTitle),
                                Label = "Title (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                              new Field {
                                Id = nameof(CaOverdueImpactDto.ImpactValue),
                                Label = "Value (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                               new Field {
                                Id = nameof(CaOverdueImpactDto.ImpactTitle2),
                                Label = "Title (id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                                new Field {
                                Id = nameof(CaOverdueImpactDto.ImpactValue2),
                                Label = "Value (id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            }
                        }
                    }
                }
            };
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return CaOverdueImpactDto.GetColumnDefinitions();
        }
    }
}