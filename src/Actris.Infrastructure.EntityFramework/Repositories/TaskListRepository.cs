using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class TaskListRepository :
        BaseCrudRepository<TX_ActionTracking,
            TaskListDto,
            TaskListDto,
            TaskListQuery>,
        ITaskListRepository
   {
      public TaskListRepository(ActrisContext context, IConnectionProvider connection, TaskListQuery tCrudQuery) : base(context, connection, tCrudQuery)
      {
      }
   }
}
