using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actris.Abstraction.Enum;
using Actris.Abstraction.Model.Request;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Services
{
    public interface ILookupService
    {
        LookupList GetDirectorateRegionalList();
        LookupList GetDivisiZonalList();
        LookupList GetWilayahKerjaList();
        LookupList GetDepartmentList();
        LookupList GetDivisionList();
        LookupList GetSubDivisionList();
        LookupList GetFunctionList();
    }
}
