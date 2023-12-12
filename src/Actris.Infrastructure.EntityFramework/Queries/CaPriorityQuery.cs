namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class MdCorrectiveActionPriorityQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  Priority,
                    PriorityBahasa,
                    PriorityDuration
            from ACTRIS.MD_CorrectiveActionPriority WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'deleted')";

        public override string CountQuery => @"select count(1) from ACTRIS.MD_CorrectiveActionPriority WITH(NOLOCK)";


        public override string LookupTextQuery => @"select Priority 
                                                    ACTRIS.MD_CorrectiveActionPriority";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_CorrectiveActionPriority WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'deleted'
                                                      ORDER BY @columnName";
    }
}
