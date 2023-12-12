using System.Threading.Tasks;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Repositories
{
   public interface ILookupRepository
   {
      Task<LookupList> GetActSourceList();
      Task<LookupList> GetCaPriorityList();
      Task<LookupList> GetCaOverdueImpactList();
      Task<LookupList> GetDirectorateList(string hierLvl2);
      Task<LookupList> GetDivisiZonaList(string hierLvl2, string directorateID);
      Task<LookupList> GetCompanyList(string hierLvl2, string directorateID, string divisionID);
      Task<LookupList> GetWilayahKerjaList(string hierLvl2, string directorateID, string divisionID, string companyCode);
   }
}
