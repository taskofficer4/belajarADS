using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using DocumentFormat.OpenXml.EMMA;

namespace Actris.Web.Controllers
{

   public class ActionTrackingReportController : BaseCrudController<ActReportDto, ActReportDto>
   {
      private IActReportService _svc;
      private ITxActService _actSvc;
      public ActionTrackingReportController(IActReportService crudSvc, ITxActService actSvc) : base(crudSvc, true)
      {
         _svc = crudSvc;
         _actSvc = actSvc;
      }

      public override Task<ActionResult> Create(ActReportDto model)
      {
         throw new System.NotImplementedException();
      }

      public override Task<ActionResult> Edit(ActReportDto model)
      {
         throw new System.NotImplementedException();
      }

      protected override FormDefinition DefineForm(FormState formState)
      {
         throw new System.NotImplementedException();
      }

      protected override List<ColumnDefinition> DefineGrid()
      {
         return ActReportDto.GetColumnDefinitions();
      }

      /// <summary>
      /// Initial Grid Render (Parent of GridList)
      /// </summary>
      /// <param name="filterList"></param>
      /// <returns></returns>
      public override PartialViewResult InitGrid(GridParam gridParam)
      {
         List<ColumnDefinition> columnDefinitions = DefineGrid();
         gridParam.ColumnDefinitions = columnDefinitions;

         gridParam.GridId = this.GetType().Name + "_grid";
         return PartialView("InitGrid", gridParam);
      }

      /// <summary>
      /// Render Grid (reload, filter, sorting)
      /// </summary>
      /// <param name="filterList"></param>
      /// <returns></returns>
      public override PartialViewResult GridList(FilterList filterList)
      {
         List<ColumnDefinition> columnDefinitions = DefineGrid();
         var task = Task.Run(async () => await _crudService.GetPaged(new GridParam
         {
            ColumnDefinitions = columnDefinitions,
            FilterList = filterList
         }));

         var ret = task.Result;
         filterList.TotalItems = ret.TotalItems;

         GridListModel model = new GridListModel
         {
            GridId = this.GetType().Name + "_grid",
            FilterList = filterList,
            ColumnDefinitions = columnDefinitions,
            HideActionButton = HideActionButton
         };
         model.FillRows(ret.Items);
         return PartialView("GridList", model);
      }

      public PartialViewResult GetCaList(string id)
      {
         var data = _actSvc.GetCaList(id);
         return PartialView("_CaList", data);
      }
   }
}
