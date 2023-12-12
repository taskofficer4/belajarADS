using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Helpers;
using ClosedXML.Excel;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Services
{
    public class ActReportService : BaseCrudService<ActReportDto, ActReportDto>, IActReportService
    {
        private readonly IActReportRepository _repo;
        public ActReportService(IActReportRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public override async Task<XLWorkbook> ExportToExcel(GridParam param)
        {
            param.FilterList.Page = 1;
            param.FilterList.Size = 1000000;
            var data = await _repo.GetPaged(param);
            return SpreadsheetGenerator.Generate("Report", data.Items, ActReportDto.GetExcelColumnDefinitions());
        }

        public ActViewDto GetActionTrackingView(string id)
        {
            return _repo.GetActionTrackingView(id);
        }

        public List<TxCaDto> GetCaList(string actionTrackingId)
        {
            var result = _repo.GetCaList(actionTrackingId);

            //foreach (var item in result)
            //{
            //    var picList = JsonSerializer.Deserialize<List<EmployeeDto>>(item.PicData);

            //    if (picList.Any())
            //    {
            //        item.Pic1 = picList[0].empName;
            //    }
               
            //}
            return result;
        }
    }
}
