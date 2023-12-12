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
    public class ImpactAnalysisController : BaseCrudController<ImpactAnalysisDto, ImpactAnalysisDto>
    {
        public ImpactAnalysisController(IImpactAnalysisService crudSvc) : base(crudSvc)
        {
        }

        public override async Task<ActionResult> Create(ImpactAnalysisDto model)
        {
            model.CreatedAt = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            return await BaseCreate(model, new ImpactAnalysisValidator(FormState.Create));
        }

        public override async Task<ActionResult> Edit(ImpactAnalysisDto model)
        {
            return await BaseUpdate(model, new ImpactAnalysisValidator(FormState.Edit));
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            return new FormDefinition
            {
                Title = "Impact Analysis",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                   new FieldSection
                    {
                        SectionName = "",
                        Fields = {
                                new Field {
                                Id = nameof(ImpactAnalysisDto.CorrectiveActionImpactAnalysisID),
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(ImpactAnalysisDto.AnalysisTitle),
                                Label = "Title (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                              new Field {
                                Id = nameof(ImpactAnalysisDto.AnalysisValue),
                                Label = "Value (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                               new Field {
                                Id = nameof(ImpactAnalysisDto.AnalysinTitle2),
                                Label = "Title (id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                              new Field {
                                Id = nameof(ImpactAnalysisDto.AnalysisValue2),
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
            return ImpactAnalysisDto.GetColumnDefinitions();
        }
    }
}