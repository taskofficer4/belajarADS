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
    public class CorrectiveActionPriorityController : BaseCrudController<MdCaPriorityDto, MdCaPriorityDto>
    {
        IMdCaPriorityService _crudSvc;
        public CorrectiveActionPriorityController(IMdCaPriorityService crudSvc) : base(crudSvc)
        {
            _crudSvc = crudSvc;
        }

        public override async Task<ActionResult> Create(MdCaPriorityDto model)
        {
            model.CreatedDate = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            return await BaseCreate(model, new MdCaPriorityValidator(FormState.Create, _crudSvc));
        }

        public override async Task<ActionResult> Edit(MdCaPriorityDto model)
        {
            return await BaseUpdate(model, new MdCaPriorityValidator(FormState.Edit, _crudSvc));
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
                        Fields ={
                             new Field {
                                Id = nameof(MdCaPriorityDto.Id),
                                Label = "Priority Old ID",
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(MdCaPriorityDto.Priority),
                                Label = "Priority",
                                FieldType = FieldType.Text,
                                IsRequired = true,
                            },
                              new Field {
                                Id = nameof(MdCaPriorityDto.PriorityBahasa),
                                Label = "Priority (Bahasa)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            }

                            ,new Field {
                                Id = nameof(MdCaPriorityDto.PriorityDuration),
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
            return MdCaPriorityDto.GetColumnDefinitions();
        }
    }
}