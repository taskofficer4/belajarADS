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
   public class OverdueImpactController : BaseCrudController<MdCaOverdueImpactDto, MdCaOverdueImpactDto>
   {
      IMdCaOverdueImpactService _crudSvc;
      public OverdueImpactController(IMdCaOverdueImpactService crudSvc) : base(crudSvc)
      {
         _crudSvc = crudSvc;
      }

      public override async Task<ActionResult> Create(MdCaOverdueImpactDto model)
      {
         model.CreatedDate = DateHelper.WibNow;
         model.CreatedBy = CurrentUser.GetPreferredUsername();
         model.DataStatus = "active";
         return await BaseCreate(model, new MdCaOverdueValidator(FormState.Create, _crudSvc));
      }

      public override async Task<ActionResult> Edit(MdCaOverdueImpactDto model)
      {
         return await BaseUpdate(model, new MdCaOverdueValidator(FormState.Edit, _crudSvc));
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
                        Fields ={
                             new Field {
                                Id = nameof(MdCaOverdueImpactDto.Id),
                                Label = "Old ID",
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(MdCaOverdueImpactDto.OverdueImpact),
                                Label = "Impact",
                                FieldType = FieldType.Text,
                                IsRequired = true,
                            },
                              new Field {
                                Id = nameof(MdCaOverdueImpactDto.ImpactBahasa),
                                Label = "Impact (Bahasa)",
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
         return MdCaOverdueImpactDto.GetColumnDefinitions();
      }
   }
}