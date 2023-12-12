namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class CaPriorityQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  CorrectiveActionPriorityID,
                    PriorityTitle,
                    PriorityValue,
                    PriorityTitle2,
                    PriorityValue2,
                    PriorityDuration
            from ACTRIS.MD_CorrectiveActionPriority WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'delete')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_CorrectiveActionPriority WITH(NOLOCK)";


        public override string LookupTextQuery => @"select SourceTitle 
                                                    ACTRIS.MD_CorrectiveActionPriority";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_CorrectiveActionPriority WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'delete'
                                                      ORDER BY @columnName";

    }
}
