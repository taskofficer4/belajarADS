namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class ActionTrackingReportQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  *
            from ACTRIS.VW_ActionTrackingReport WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "";

        public override string CountQuery => @"
            select count(1) from ACTRIS.VW_ActionTrackingReport WITH(NOLOCK)";


        public override string LookupTextQuery => @"select SourceTitle 
                                                    ACTRIS.VW_ActionTrackingReport";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.VW_ActionTrackingReport WITH (NOLOCK)
                                                      ORDER BY @columnName";
    }
}
