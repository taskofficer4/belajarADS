using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        public LookupList GetDepartmentList()
        {
            return DummyOptionList("Department");
        }

        public LookupList GetDirectorateRegionalList()
        {
            return DummyOptionList("Regional");
        }

        public LookupList GetDivisionList()
        {
            return DummyOptionList("Division");
        }

        public LookupList GetDivisiZonalList()
        {
            return DummyOptionList("Divisi Zona");
        }

        public LookupList GetFunctionList()
        {
            return DummyOptionList("Function");
        }

        public LookupList GetSubDivisionList()
        {
            return DummyOptionList("Sub Division");
        }

        public LookupList GetWilayahKerjaList()
        {
            return DummyOptionList("Wilayah Kerja");
        }

        private LookupList DummyOptionList(string prefix)
        {
            var ls = new LookupList
            {
                Items = new List<LookupItem>()
            };

            for (int i = 1; i <= 10; i++)
            {
                ls.Items.Add(new LookupItem
                {
                    Text = $"{prefix} {i}",
                    Value = $"{prefix}_{i}",
                });
            }
            return ls;
        }
    }
}
