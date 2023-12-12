using ClosedXML.Excel;
using System.Threading.Tasks;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Services
{
    public interface ICrudService<TEntity, TGridModel>
    {
        Task<TEntity> GetOne(string id);
        Task Update(TEntity model);
        Task Delete(string id);
        Task Create(TEntity model);
        Task<string> GetLookupText(int id);
        Task<Paged<TGridModel>> GetPaged(GridParam param);
        Task<LookupList> GetAdaptiveFilterList(string columnId, ColumnType columnType);
        Task<XLWorkbook> ExportToExcel(GridParam param);
        byte[] ExportToPDF(GridListModel model, string headerText, int[] tableHeaderSizes);
    }
}
