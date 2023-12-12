namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class TaskListOverdueQuery : BaseCrudQuery
    {

        public override string SelectPagedQuery => $@"
           SELECT *
           FROM [actris].VW_TaskListOverdue";

		public override string SelectPagedQueryAdditionalWhere =>
	 "ManagerEmpAccount = '@CurrentUser'";

		public override string CountQuery => $@"
            select count(1) FROM [actris].VW_TaskListOverdue";


        public override string LookupTextQuery => @"";

        public override string AdaptiveFilterQuery => $@"SELECT DISTINCT @columnName
                                                     FROM [actris].VW_TaskListOverdue
                                                      ORDER BY @columnName";
    }
}
