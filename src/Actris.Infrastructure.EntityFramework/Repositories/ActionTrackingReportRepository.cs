using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
    public class ActionTrackingReportRepository : 
        BaseCrudRepository<TX_ActionTracking, 
            ActReportDto, 
            ActReportDto, 
            ActionTrackingReportQuery>, 
        IActReportRepository
    {
        public ActionTrackingReportRepository(ActrisContext context, IConnectionProvider connection, ActionTrackingReportQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }

        public List<TxCaDto> GetCaList(string actionTrackingId)
        {
            //var result = _context.TX_CorrectiveAction
            //     .Where(o=>o.ActionTrackingID == actionTrackingId)
            //     .ToListWithNoLock()
            //     .Select(o=> new CorrectiveActionDto(o))
            //     .ToList();



            // return result;
            return null;
        }

        public ActViewDto GetActionTrackingView(string id)
        {
            //var model = _context.TX_ActionTracking.FirstOrDefaultWithNoLock(o=>o.ActionTrackingID == id);
            //var dto = new ActionTrackingViewDto
            //{
            //    ActId = model.ActionTrackingReference,
            //    ActSource = model.MD_ActionTrackingSource.SourceTitle,
            //    ReferenceId= model.AdditionalData,
            //    IssueDate = model.IssueDate,
            //    FindingDescription = model.FindingDesc,
            //    TypeShuRegion = model.TrypeShuRegion,
            //    DirectorateRegional = model.DirectorateRegionalDesc,
            //    DivisionZona = model.DivisiZonaDesc,
            //    Company = model.CompanyName,
            //    WilayahKerja = model.WilayahkerjaDesc,
            //    Location = model.Location,
            //    SubLocation= model.SubLocation,
            //    LocationStatus = model.LocationStatusID,
            //    Nct = model.Nct,
            //    Rca = model.Rca
            //};

            //dto.ListAction = new List<CaListViewDto>();

            //foreach (var ca in model.TX_CorrectiveAction.Where(o=>o.DataStatus != "deleted"))
            //{
            //    var caDto = new CaListViewDto
            //    {
            //        IsApprove= false,
            //        IsReject= false,
            //        Remarks = "",
            //        CaId = ca.CorrectiveActionReference,
            //        Department = ca.ResponsibleDepartment,
            //        Recomendation = ca.Recomendation,
            //        ListPic = new List<string>(),
            //        PriorityLevel = ca.MD_CorrectiveActionPriority.PriorityTitle,
            //        PendingAction = ""
            //    };

            //    var picList = JsonConvert.DeserializeObject<List<EmployeeDto>>(ca.PicData);

            //    if (picList.Any())
            //    {
            //        foreach (var pic in picList)
            //        {
            //            caDto.ListPic.Add(pic.empName);
            //        }

            //    }
            //    dto.ListAction.Add(caDto);
            //}
            //return dto;
            return null;
        }


        public override async Task Delete(string id)
        {
            //var item = _context.TX_ActionTracking.FirstOrDefault(o => o.ac == id);
            //if (item == null)
            //{
            //    throw new Exception($"Data with id '{id}' not found or already deleted");
            //}
            //item.DataStatus = "deleted";
            //await _context.SaveChangesAsync();
        }
    }
}
