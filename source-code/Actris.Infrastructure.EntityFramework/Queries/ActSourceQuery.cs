namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class ActSourceQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  ActionTrackingSourceID,
                    SourceTitle,
                    SourceValue,
                    SourceTitle2,
                    SourceValue2,
                    DirectorateRegionalDesc,
                    DivisiZonaDesc,
                    WilayahkerjaDesc,
                    DepartmentDesc,
                    DivisionDesc,
                    SubDivisionDesc,
                    FunctionDesc
            from ACTRIS.MD_ActionTrackingSource WITH(NOLOCK)";

        public override string SelectPagedQueryAdditionalWhere => "(DataStatus is null OR DataStatus != 'delete')";

        public override string CountQuery => @"
            select count(1) from ACTRIS.MD_ActionTrackingSource WITH(NOLOCK)";


        public override string LookupTextQuery => @"select SourceTitle 
                                                    ACTRIS.MD_ActionTrackingSource";

        public override string AdaptiveFilterQuery => @"SELECT DISTINCT @columnName
                                                      FROM  ACTRIS.MD_ActionTrackingSource WITH (NOLOCK)
                                                      WHERE DataStatus is null OR DataStatus != 'delete'
                                                      ORDER BY @columnName";
    }
}
