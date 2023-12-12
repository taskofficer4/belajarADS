using Actris.Abstraction.Helper;

namespace Actris.Abstraction.Model.Entities
{
    public partial class MD_ActionTrackingSource
    {
        public string ToKeyString()
        {
            return ActSourceKeyHelper.ToKeyString(Source, DirectorateID, DivisionID, SubDivisionID, DepartmentID);
        }

        public void FromKeyString(string strKey)
        {
            (Source, DirectorateID, DivisionID, SubDivisionID, DepartmentID) = ActSourceKeyHelper.FromKeyString(strKey);
        }
    }
}
