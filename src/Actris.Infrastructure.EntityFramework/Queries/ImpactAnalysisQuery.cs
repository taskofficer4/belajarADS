namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class ImpactAnalysisQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  CorrectiveActionImpactAnalysisID,
                    AnalysisTitle,
                    AnalysisValue,
                    AnalysinTitle2,
                    AnalysisValue2
            from ACTRIS.MD_CorrectiveActionImpactAnalysis WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'delete')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_CorrectiveActionImpactAnalysis WITH(NOLOCK)";


        public override string LookupTextQuery => @"select AnalysisTitle 
                                                    ACTRIS.MD_CorrectiveActionImpactAnalysis";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_CorrectiveActionImpactAnalysis WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'delete'
                                                      ORDER BY @columnName";


    }
}
