using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class TaskListOverdueRepository :
        BaseCrudRepository<TX_ActionTracking,
            TaskListDto,
            TaskListDto,
            TaskListOverdueQuery>,
        ITaskListOverdueRepository
   {
      public TaskListOverdueRepository(ActrisContext context, IConnectionProvider connection,
         TaskListOverdueQuery tCrudQuery) : base(context, connection, tCrudQuery)
      {
      }
   }
}
