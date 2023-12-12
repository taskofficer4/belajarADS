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
    public class ActionTrackingSourceController : BaseCrudController<MdActSourceDto, MdActSourceDto>
    {
        private ILookupService _lookUpSvc;
        public ActionTrackingSourceController(IMdActSourceService crudSvc, ILookupService lookUpSvc) : base(crudSvc)
        {
            _lookUpSvc = lookUpSvc;
        }

        public override async Task<ActionResult> Create(MdActSourceDto model)
        {
            model.Lookup = await LoadLookupList();
            model.CreatedDate = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.FillLookupDesc();
            model.DataStatus = "active";

            return await BaseCreate(model, new MdActSourceValidator(FormState.Create));
        }

        public override async Task<ActionResult> Edit(MdActSourceDto model)
        {
            model.Lookup = await LoadLookupList();
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            model.FillLookupDesc();
            return await BaseUpdate(model, new MdActSourceValidator(FormState.Edit));
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            var lookup = Task.Run(async () => await LoadLookupList()).Result;

            return new FormDefinition
            {
                Title = "ACT Source",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                   new FieldSection
                    {
                        SectionName = "",
                        Fields = {
                                new Field {
                                Id = nameof(MdActSourceDto.CompositeKey),
                                FieldType = FieldType.Hidden,
                                Label = "compositeKey",
                                IsRequired = true
                            },
                                new Field {
                                Id = nameof(MdActSourceDto.Source),
                                FieldType = FieldType.Text,
                                Label = "Source",
                                IsRequired = true
                            },
                            new Field {
                                Id = nameof(MdActSourceDto.SourceBahasa),
                                Label = "Source (Bahasa)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                            new Field {
                                Id = nameof(MdActSourceDto.DirectorateID),
                                Label = "Directorate",
                                FieldType = FieldType.Dropdown,
                                IsRequired = true,
                                LookUpList = lookup.Directorate,
                            },
                            new Field {
                                Id = nameof(MdActSourceDto.DivisionID),
                                Label = "Divisi ",
                                FieldType = FieldType.Dropdown,
                                IsRequired = true,
                                LookUpList = lookup.Division,
                            },
                             new Field {
                                Id = nameof(MdActSourceDto.SubDivisionID),
                                Label = "Sub Division",
                                FieldType = FieldType.Dropdown,
                                IsRequired = true,
                                LookUpList = lookup.SubDivision
                            },
                             new Field {
                                Id = nameof(MdActSourceDto.DepartmentID),
                                Label = "Department",
                                FieldType = FieldType.Dropdown,
                                IsRequired = true,
                                LookUpList = lookup.Department
                            }
                        }
                    }
                }
            };
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return MdActSourceDto.GetColumnDefinitions();
        }

        private async Task<ActSourceLookupList> LoadLookupList()
        {
            var lookup = new ActSourceLookupList();
            lookup.Directorate = await _lookUpSvc.GetDirectorateList();
            lookup.Division = await _lookUpSvc.GetDivisionList();
            lookup.SubDivision = await _lookUpSvc.GetSubDivisionList();
            lookup.Department = await _lookUpSvc.GetDepartmentList();
            return lookup;
        }
    }
}