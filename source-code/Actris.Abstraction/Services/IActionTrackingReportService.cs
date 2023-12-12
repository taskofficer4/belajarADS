using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;

namespace Actris.Abstraction.Services
{
    public interface IActionTrackingReportService : ICrudService<ActionTrackingReportDto, ActionTrackingReportDto>
    {
        List<CorrectiveActionDto> GetCaList(string actionTrackingId);
        ActionTrackingViewDto GetActionTrackingView(string id);

    }
}
