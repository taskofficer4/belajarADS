using Actris.Abstraction.Model.Aiman;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Services
{
    public interface IAimanApiService
    {
        Task<List<AimanDirectorate>> GetDirectorate(string whereCondition = "1=1");
        Task<List<AimanDivision>> GetDivision();
        Task<List<AimanSubDivision>> GetSubDivision();
        Task<List<AimanCompany>> GetCompany();
        Task<List<AimanWorkingArea>> GetWorkingArea();
        Task<List<AimanLocationCompany>> GetLocationCompany();
        Task<List<AimanEmployee>> GetAllMasterEmployee(string whereCondition);
        Task<List<AimanDepartment>> GetDepartment();
        Task<List<AimanPsaHierarchy>> GetPSAHierarchy(string whereCondition);

    }
}
