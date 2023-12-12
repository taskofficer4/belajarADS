using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class TaskListNeedApprovalService : BaseCrudService<TaskListDto, TaskListDto>, ITaskListNeedApprovalService
   {
      public TaskListNeedApprovalService(ITaskListNeedApprovalRepository repo) : base(repo)
      {
      }
   }
}
