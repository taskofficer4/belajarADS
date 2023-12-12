namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class TaskListQuery : BaseCrudQuery
    {

        public override string SelectPagedQuery => $@"
           SELECT *
           FROM [actris].VW_TaskListNeedAction";

        public override string SelectPagedQueryAdditionalWhere => 
            "Pic1EmpAccount = '@CurrentUser' OR " +
			"Pic2EmpAccount = '@CurrentUser'";

        public override string CountQuery => $@"
            select count(1) FROM [actris].VW_TaskListNeedAction";


        public override string LookupTextQuery => @"";

        public override string AdaptiveFilterQuery => $@"SELECT DISTINCT @columnName
                                                     FROM [actris].VW_TaskListNeedAction
                                                      ORDER BY @columnName";
    }
}
