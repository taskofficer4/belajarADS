namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class CaOverdueImpactQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  CorrectiveActionOverdueImpactID,
                    ImpactTitle,
                    ImpactValue,
                    ImpactTitle2,
                    ImpactValue2
            from ACTRIS.MD_CorrectiveActionOverdueImpact WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'delete')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_CorrectiveActionOverdueImpact WITH(NOLOCK)";


        public override string LookupTextQuery => @"select ImpactTitle 
                                                    ACTRIS.MD_CorrectiveActionOverdueImpact";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_CorrectiveActionOverdueImpact WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'delete'
                                                      ORDER BY @columnName";

    }
}
