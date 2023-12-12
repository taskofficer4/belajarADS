using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class TaskListNeedApprovalRepository :
        BaseCrudRepository<TX_ActionTracking,
            TaskListDto,
            TaskListDto,
            TaskListNeedApprovalQuery>,
        ITaskListNeedApprovalRepository
   {
      public TaskListNeedApprovalRepository(ActrisContext context, IConnectionProvider connection, TaskListNeedApprovalQuery tCrudQuery) : base(context, connection, tCrudQuery)
      {
      }
   }
}
