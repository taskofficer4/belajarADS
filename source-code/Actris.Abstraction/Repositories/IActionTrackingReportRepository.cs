using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Repositories
{
    public interface IActionTrackingReportRepository : ICrudRepository<ActionTrackingReportDto, ActionTrackingReportDto>
    {
        List<CorrectiveActionDto> GetCaList(string actionTrackingId);
        ActionTrackingViewDto GetActionTrackingView(string id);
    }
}
