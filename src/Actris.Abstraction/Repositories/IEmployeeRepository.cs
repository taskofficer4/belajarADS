using System.Collections.Generic;
using System.Threading.Tasks;
using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Repositories
{
   public interface IEmployeeRepository
   {
      Task<List<VwEmployeeLs>> GetAllManager();
      Task<List<VwEmployeeLs>> GetEmployeeById(string empID);
      Task<Select2AjaxResponse> GetEmployeeByParent(string parentPosID, string keyword, int page);
      Task<TxCaUserDto> GetOneEmployee(string empID);
      Task<TxCaUserDto> GetOneEmployeeByPosId(string PosID);

     //Get Executive Manager/ VP
      Task<TxCaUserDto> GetExecutiveEmployee(string PosID);

    }
}
