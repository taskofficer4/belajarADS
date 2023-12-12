namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class TxActionTrackingQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select *
            from ACTRIS.TX_ActionTracking WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'deleted') AND Status = 'Draft'";

        public override string CountQuery => @"
            select count(1) from ACTRIS.TX_ActionTracking WITH(NOLOCK)";


        public override string LookupTextQuery => @"select ActionTrackingReference 
                                                    ACTRIS.TX_ActionTracking";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM ACTRIS.TX_ActionTracking WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'deleted'
                                                      ORDER BY @columnName";
    }
}
