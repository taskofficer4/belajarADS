using System.Threading.Tasks;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Repositories
{
    public interface ICrudRepository<TDto, TGridModel>
    {
        Task Create(TDto model);
        Task Update(TDto model);
        Task Delete(string id);
        Task<Paged<TGridModel>> GetPaged(GridParam param);
        Task<TDto> GetOne(string id);
        Task<string> GetLookupText(int id);
        Task<LookupList> GetAdaptiveFilterList(string columnId, ColumnType columnType);
    }
}
