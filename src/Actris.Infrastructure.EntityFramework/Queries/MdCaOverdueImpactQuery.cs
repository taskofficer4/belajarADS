namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class MdCaOverdueImpactQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  OverdueImpact,
                    ImpactBahasa,
                    DataStatus,
                    CreatedDate,
                    CreatedBy
            from ACTRIS.MD_CorrectiveActionOverdueImpact WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'deleted')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_CorrectiveActionOverdueImpact WITH(NOLOCK)";


        public override string LookupTextQuery => @"select OverdueImpact 
                                                    ACTRIS.MD_CorrectiveActionOverdueImpact";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_CorrectiveActionOverdueImpact WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'deleted'
                                                      ORDER BY @columnName";

    }
}
