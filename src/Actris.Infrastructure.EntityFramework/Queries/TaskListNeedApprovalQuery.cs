namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class TaskListNeedApprovalQuery : BaseCrudQuery
    {

        public override string SelectPagedQuery => $@"
           SELECT *
           FROM [actris].VW_TaskListNeedApproval";
        public override string SelectPagedQueryAdditionalWhere =>
            "ManagerEmpAccount = '@CurrentUser'";
		public override string CountQuery => $@"
            select count(1) FROM [actris].VW_TaskListNeedApproval";


        public override string LookupTextQuery => @"";

        public override string AdaptiveFilterQuery => $@"SELECT DISTINCT @columnName
                                                     FROM [actris].VW_TaskListNeedApproval
                                                      ORDER BY @columnName";
    }
}
