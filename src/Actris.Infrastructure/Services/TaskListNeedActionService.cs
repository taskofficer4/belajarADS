using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class TaskListNeedActionService : BaseCrudService<TaskListDto, TaskListDto>, ITaskListNeedActionService
   {
      public TaskListNeedActionService(ITaskListRepository repo) : base(repo)
      {
      }
   }
}
