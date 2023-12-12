using Actris.Abstraction.Model.Entities;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Drawing;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Actris.Infrastructure.Services
{
    public class BaseCrudService<TDto, TGridModel> : ICrudService<TDto,TGridModel>
    {
        private readonly ICrudRepository<TDto, TGridModel> _repo;
        public BaseCrudService(ICrudRepository<TDto, TGridModel> repo)
        {
            _repo = repo;

        }

        public Task<Paged<TGridModel>> GetPaged(GridParam param)
        {
        
            return _repo.GetPaged(param);
        }

        public Task<LookupList> GetAdaptiveFilterList(string columnId, List<ColumnDefinition> columnDefinitions)
        {
          
            var column = columnDefinitions.FirstOrDefault(o => o.Id == columnId);
            return _repo.GetAdaptiveFilterList(columnId, column.Type);
        }

        public virtual async Task<XLWorkbook> ExportToExcel(GridParam param)
        {

            param.FilterList.Page = 1;
            param.FilterList.Size = 10000;

            var data = await _repo.GetPaged(param);
            return SpreadsheetGenerator.Generate("Report", data.Items, param.ColumnDefinitions);
        }

        public byte[] ExportToPDF(GridListModel gridList, string headerText, int[] tableHeaderSizes)
        {

            PDFTableGenerator pdfTable = new PDFTableGenerator(gridList, headerText, tableHeaderSizes);
            return pdfTable.GeneratePdf();
        }

        public async Task Create(TDto model)
        {
            await _repo.Create(model);
        }

        public async Task Delete(string id)
        {
            await _repo.Delete(id);
        }

        public async Task<TDto> GetOne(string id)
        {
            return await _repo.GetOne(id);
        }

        public async Task Update(TDto model)
        {
            await _repo.Update(model);
        }

        public async Task<string> GetLookupText(int id)
        {
            return await _repo.GetLookupText(id);
        }

        public async Task<LookupList> GetAdaptiveFilterList(string columnId, ColumnType columnType)
        {
            return await _repo.GetAdaptiveFilterList(columnId, columnType);
        }
    }
}
