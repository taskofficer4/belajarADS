namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class ActSourceQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  *
            from ACTRIS.MD_ActionTrackingSource WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'deleted')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_ActionTrackingSource WITH(NOLOCK)";


        public override string LookupTextQuery => @"select SourceTitle 
                                                    ACTRIS.MD_ActionTrackingSource";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_ActionTrackingSource WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'deleted'
                                                      ORDER BY @columnName";
    }
}
