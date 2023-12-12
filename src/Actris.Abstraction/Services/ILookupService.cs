using System.Collections.Generic;
using System.Threading.Tasks;
using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Services
{
   public interface ILookupService
   {
      // untuk MasterData/Act Source
      Task<LookupList> GetActSourceList();
      Task<LookupList> GetCaPriorityList();
      Task<LookupList> GetDirectorateList();
      Task<LookupList> GetDivisionList();
      Task<LookupList> GetSubDivisionList();
      Task<LookupList> GetCompanyList();
      Task<LookupList> GetWilayahKerjaList();
      Task<LookupList> GetDepartmentList();
      Task<LookupList> GetLocationCompanyList(string companyCode);

      // utk act form
      Task<LookupList> GetDirectorateList(string hierLvl2);
      Task<LookupList> GetDivisiZonaList(string hierLvl2, string directorateID);
      Task<LookupList> GetCompanyList(string hierLvl2, string directorateID, string divisionID);
      Task<LookupList> GetWilayahKerjaList(string hierLvl2, string directorateID, string divisionID, string companyCode);

      // untuk ca form
      Task<List<VwEmployeeLs>> GetEmployeeList(string empID);
      Task<Select2AjaxResponse> GetManagerList(string keyword, int page = 1);
      Task<Select2AjaxResponse> GetEmployeeListByParentEmpID(string parentEmpID, string keyword, int page = 1);

      // untuk ca overdue
      Task<LookupList> GetCaOverdueImpactList();
   }
}
