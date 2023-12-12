using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Services
{
   public interface ITxCaService
   {
      Task<TxCaDto> GetOne(string caId);
      Task SaveFollowUp(TxCaDto dto);
      Task SubmitFollowUp(TxCaDto dto);
      Task SubmitProposedDueDate(TxCaDto dto);
      Task SubmitOverdue(TxCaDto dto);
      void Update(TxCaDto dto);
      Task SubmitApproval(TxCaDto dto);
      Task SubmitForCompletion(TxCaDto dto);
      Task SubmitApprovalCompleted(TxCaDto dto);

      Task SubmitApprovalDueDate(TxCaDto dto);

    }
}
