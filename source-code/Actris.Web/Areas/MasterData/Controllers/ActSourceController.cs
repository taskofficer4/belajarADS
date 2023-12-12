using Actris.Abstraction.Enum;
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
    public class ActSourceController : BaseCrudController<ActSourceDto, ActSourceDto>
    {
        private ILookupService _lookUpSvc;
        public ActSourceController(IActSourceService crudSvc, ILookupService lookUpSvc) : base(crudSvc)
        {
            _lookUpSvc = lookUpSvc;
        }

        public override async Task<ActionResult> Create(ActSourceDto model)
        {
            LoadLookupList(model);
            model.CreatedAt = DateHelper.WibNow;
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.FillLookupDesc();
            model.DataStatus = "active";

            return await BaseCreate(model, new ActSourceValidator(FormState.Create));
        }

        public override async Task<ActionResult> Edit(ActSourceDto model)
        {
            LoadLookupList(model);
            model.CreatedBy = CurrentUser.GetPreferredUsername();
            model.DataStatus = "active";
            model.FillLookupDesc();
            return await BaseUpdate(model, new ActSourceValidator(FormState.Edit));
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
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
                                Id = nameof(ActSourceDto.ActionTrackingSourceID),
                                FieldType = FieldType.Hidden
                            },
                            new Field {
                                Id = nameof(ActSourceDto.SourceTitle),
                                Label = "Title (En)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                               new Field {
                                Id = nameof(ActSourceDto.SourceTitle2),
                                Label = "Title (Id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                            new Field {
                                Id = nameof(ActSourceDto.SourceValue),
                                Label = "Value (En)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                             new Field {
                                Id = nameof(ActSourceDto.SourceValue2),
                                Label = "Value (Id)",
                                FieldType = FieldType.Text,
                                IsRequired = true
                            },
                            new Field {
                                Id = nameof(ActSourceDto.DirectorateRegionalID),
                                Label = "Directorate Regional",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetDirectorateRegionalList()
                            },
                            new Field {
                                Id = nameof(ActSourceDto.DivisiZonaID),
                                Label = "Divisi Zona",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetDivisiZonalList()
                            },
                             new Field {
                                Id = nameof(ActSourceDto.WilayahkerjaID),
                                Label = "Wilayah kerja",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetWilayahKerjaList()
                            },
                             new Field {
                                Id = nameof(ActSourceDto.DepartmentID),
                                Label = "Department",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetDepartmentList()
                            },
                              new Field {
                                Id = nameof(ActSourceDto.DivisionID),
                                Label = "Division",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetDivisionList()
                            },
                              new Field {
                                Id = nameof(ActSourceDto.SubDivisionID),
                                Label = "Sub Division",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetSubDivisionList()
                            },
                               new Field {
                                Id = nameof(ActSourceDto.FunctionID),
                                Label = "Function",
                                FieldType = FieldType.Dropdown,
                                IsRequired = false,
                                LookUpList = _lookUpSvc.GetFunctionList()
                            },
                        }
                    }
                }
            };
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return ActSourceDto.GetColumnDefinitions();
        }

        private void LoadLookupList(ActSourceDto dto)
        {
            dto.Lookup = new ActSourceLookupList();
            dto.Lookup.DirectorateRegional = _lookUpSvc.GetDirectorateRegionalList();
            dto.Lookup.DivisiZona = _lookUpSvc.GetDivisionList();
            dto.Lookup.WilayahKerja= _lookUpSvc.GetWilayahKerjaList();
            dto.Lookup.Department= _lookUpSvc.GetDepartmentList();
            dto.Lookup.Division = _lookUpSvc.GetDivisionList();
            dto.Lookup.SubDivision= _lookUpSvc.GetSubDivisionList();
            dto.Lookup.Function = _lookUpSvc.GetFunctionList();
        }
    }
}