using Serilog;
using System.Web;
using System.Web.Mvc;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Services;
using Actris.Web.Providers;
using Unity;
using Unity.Mvc5;
using Actris.Infrastructure.EntityFramework.Repositories;

namespace Actris.Web
{
   public static class UnityConfig
   {
      public static void RegisterComponents()
      {
         var container = new UnityContainer();

         container.RegisterType<ActrisContext, DbContextMapper>();
         container.RegisterType<IConnectionProvider, ConnectionStringProvider>();
         container.RegisterFactory<ILogger>((ctr, type, name) =>
         {
            ILogger log = new LoggerConfiguration()
                   .ReadFrom.AppSettings()
                   .CreateLogger();

            return log;
         });
         container.RegisterFactory<HttpContextBase>((_) =>
         {
            return new HttpContextWrapper(HttpContext.Current);
         });


         container.RegisterType<IUserService, UserService>();

         container.RegisterType<IMdActSourceRepository, ActSourceRepository>();
         container.RegisterType<IMdActSourceService, MdActSourceService>();

         container.RegisterType<IMdCaOverdueImpactRepository, MdCaOverdueImpactRepository>();
         container.RegisterType<IMdCaOverdueImpactService, MdCaOverdueImpactService>();

         container.RegisterType<IMdCaPriorityRepository, MdCaPriorityRepository>();
         container.RegisterType<IMdCaPriorityService, MdCaPriorityService>();

         container.RegisterType<IActReportRepository, ActionTrackingReportRepository>();
         container.RegisterType<IActReportService, ActReportService>();

         container.RegisterType<ILookupRepository, LookupRepository>();
         container.RegisterType<ILookupService, LookupService>();

         container.RegisterType<ITxActRepository, TxActRepository>();
         container.RegisterType<ITxActService, TxActService>();

         container.RegisterType<IAimanApiService, AimanApiService>();
         container.RegisterType<ITxAttachmentRepository, TxAttachmentRepository>();
         container.RegisterType<IFileAttachmentService, FileAttachmentService>();

         container.RegisterType<ITxCaRepository, TxCaRepository>();
         container.RegisterType<IEmployeeRepository, EmployeeRepository>();


         container.RegisterType<ITaskListRepository, TaskListRepository>();
         container.RegisterType<ITaskListService, TaskListService>();

         container.RegisterType<ITaskListNeedActionService, TaskListNeedActionService>();

         container.RegisterType<ITaskListOverdueRepository, TaskListOverdueRepository>();
         container.RegisterType<ITaskListOverdueService, TaskListOverdueService>();

         container.RegisterType<ITaskListNeedApprovalRepository, TaskListNeedApprovalRepository>();
         container.RegisterType<ITaskListNeedApprovalService, TaskListNeedApprovalService>();

         container.RegisterType<ITxCaService, TxCaService>();

         container.RegisterType<IWorkflowService, WorkflowService>();
         DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      }
   }
}