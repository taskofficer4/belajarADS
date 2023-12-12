using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;

namespace Actris.Abstraction.Services
{
    public interface IActReportService : ICrudService<ActReportDto, ActReportDto>
    {
        List<TxCaDto> GetCaList(string actionTrackingId);
        ActViewDto GetActionTrackingView(string id);

    }
}
