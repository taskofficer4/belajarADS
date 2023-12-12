using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Repositories
{
   public interface ITxCaRepository : ICrudRepository<TxCaDto, TxCaDto>
   {
      Task<List<TxCaDto>> GetList(string actionTrackingReference);
      Task Sync(string actId, List<TxCaDto> caList, bool isSubmit);
      Task SaveFollowUp(TxCaDto dto);
      Task SaveProposedDueDate(TxCaDto dto);
      Task SaveOverdue(TxCaDto dto);

      Task SaveWorkflowResponse(TxCaDto dto);
   }
}
