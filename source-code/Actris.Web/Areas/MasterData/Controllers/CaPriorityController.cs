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
    public class CaPriorityController : BaseCrudController<CaPriorityDto, CaPriorityDto>
    {
        public CaPriorityController(ICaPriorityService crudSvc) : base(crudSvc)
        {
        }

        public override async Task<ActionResult> Create(CaPriorityDto model)
        {
            model.CreatedAt = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            return await BaseCreate(model, new CaPriorityValidator(FormState.Create));
        }

        public override async Task<ActionResult> Edit(CaPriorityDto model)
        {
            return await BaseUpdate(model, new CaPriorityValidator(FormState.Edit));
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            return new FormDefinition
            {
                Title = "Priority Level",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                   new FieldSection
                    {
                        SectionName = "",
                        Fields = {
                                new Field {
                                Id = nameof(CaPriorityDto.CorrectiveActionPriorityID),
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(CaPriorityDto.PriorityTitle),
                                Label = "Title (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                              new Field {
                                Id = nameof(CaPriorityDto.PriorityValue),
                                Label = "Value (en)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                               new Field {
                                Id = nameof(CaPriorityDto.PriorityTitle2),
                                Label = "Title (id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                                new Field {
                                Id = nameof(CaPriorityDto.PriorityValue2),
                                Label = "Value (id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                                   new Field {
                                Id = nameof(CaPriorityDto.PriorityDuration),
                                Label = "Duration",
                                FieldType = FieldType.Number,
                                IsRequired = false
                            }

                        }
                    }
                }
            };
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return CaPriorityDto.GetColumnDefinitions();
        }
    }
}