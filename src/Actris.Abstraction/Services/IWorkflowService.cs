using System.Threading.Tasks;
using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Services
{
   public interface IWorkflowService
   {
      Task<TrxMadam.Object> GetMadamStatusInfo(string TransNo);
      Task<TrxMadam.ResponseTrx> DoMadamTrx(TrxMadam.DoTrx trx);
   }

}