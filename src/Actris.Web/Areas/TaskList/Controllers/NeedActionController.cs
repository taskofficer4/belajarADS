using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Web.Controllers;

namespace Actris.Web.Areas.TaskList.Controllers
{

   public class NeedActionController : TaskListBaseController
   {
      public NeedActionController(ITaskListNeedActionService crudSvc,
         ITaskListNeedActionService needActionSvc,
         ITaskListNeedApprovalService needApprovalSvc,
         ITaskListOverdueService overdueSvc) : base(crudSvc,needActionSvc, needApprovalSvc,overdueSvc)
      {
      }

     
   }
}
