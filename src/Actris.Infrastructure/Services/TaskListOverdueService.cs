using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class TaskListOverdueService : BaseCrudService<TaskListDto, TaskListDto>, ITaskListOverdueService
   {
      public TaskListOverdueService(ITaskListOverdueRepository repo) : base(repo)
      {
      }
   }
}
