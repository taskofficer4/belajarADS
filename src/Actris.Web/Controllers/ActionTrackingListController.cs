using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;

namespace Actris.Web.Controllers
{

    public class ActionTrackingListController : BaseCrudController<TxActDto, TxActDto>
    {
        private ITxActService _svc;
        public ActionTrackingListController(ITxActService crudSvc) : base(crudSvc,true)
        {
            _svc = crudSvc;
        }
        
        public override ActionResult Index()
        {
            ViewBag.Message = "";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
           
            return base.Index();
        }

        public override Task<ActionResult> Create(TxActDto model)
        {
            throw new System.NotImplementedException();
        }

        public override Task<ActionResult> Edit(TxActDto model)
        {
            throw new System.NotImplementedException();
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            throw new System.NotImplementedException();
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return TxActDto.GetColumnDefinitions();
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
                HideExportButton = true
            };
            model.FillRows(ret.Items);
            return PartialView("GridList", model);
        }



        public PartialViewResult GetCaList(string id)
        {
            var data = _svc.GetCaList(id);
            return PartialView("_CaList", data);
        }

        //[HttpGet]
        //public override async Task<ActionResult> Detail(string id)
        //{
        //    ActViewDto data = _svc.GetActionTrackingView(id);
        //    return View(data);
        //}
    }
}
