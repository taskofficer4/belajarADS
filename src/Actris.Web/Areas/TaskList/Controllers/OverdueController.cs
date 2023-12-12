using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Web.Controllers;
using System.Collections.Generic;

namespace Actris.Web.Areas.TaskList.Controllers
{

   public class OverdueController : TaskListBaseController
   {
      public OverdueController(ITaskListOverdueService crudSvc,
       ITaskListNeedActionService needActionSvc,
       ITaskListNeedApprovalService needApprovalSvc,
       ITaskListOverdueService overdueSvc) : base(crudSvc, needActionSvc, needApprovalSvc, overdueSvc)
      {
      }


   }
}
