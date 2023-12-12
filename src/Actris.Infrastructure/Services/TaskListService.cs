using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class TaskListService : BaseCrudService<TaskListDto, TaskListDto>, ITaskListService
   {
      public TaskListService(ITaskListRepository repo) : base(repo)
      {
      }
   }
}
