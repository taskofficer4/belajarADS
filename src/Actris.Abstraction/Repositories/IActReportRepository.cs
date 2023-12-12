using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Repositories
{
    public interface IActReportRepository : ICrudRepository<ActReportDto, ActReportDto>
    {
        List<TxCaDto> GetCaList(string actionTrackingId);
        ActViewDto GetActionTrackingView(string id);
    }
}
