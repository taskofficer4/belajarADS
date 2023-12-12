using Actris.Abstraction.Helper;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Util;

namespace Actris.Abstraction.Model.Dto
{
    public class MdActSourceDto : BaseDtoAutoMapper<MD_ActionTrackingSource>
    {
        public string CompositeKey { get; set; }
        public string Source { get; set; }
        public string SourceBahasa { get; set; }
        public string DataStatus { get; set; }
        public string DirectorateID { get; set; }
        public string DirectorateDesc { get; set; }
        public string DivisionID { get; set; }
        public string DivisionDesc { get; set; }
        public string SubDivisionID { get; set; }
        public string SubDivisionDesc { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentDesc { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TX_ActionTracking> TX_ActionTracking { get; set; }


        public ActSourceLookupList Lookup { get; set; }


        public MdActSourceDto()
        {
        }

        public MdActSourceDto(MD_ActionTrackingSource entity) : base(entity)
        {
        }

      
        public string ToKeyString()
        {
            return ActSourceKeyHelper.ToKeyString(Source, DirectorateID, DivisionID, SubDivisionID, DepartmentID);
        }

        public void FromKeyString(string strKey)
        {
            (Source, DirectorateID, DivisionID, SubDivisionID, DepartmentID) = ActSourceKeyHelper.FromKeyString(strKey);
        }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Key", nameof(CompositeKey), ColumnType.Id),
                new ColumnDefinition("Source", nameof(Source), ColumnType.String),
                new ColumnDefinition("Source (Bahasa)", nameof(SourceBahasa), ColumnType.String),
                new ColumnDefinition("Directorate", nameof(DirectorateDesc), ColumnType.String),
                new ColumnDefinition("Division", nameof(DivisionDesc), ColumnType.String),
                new ColumnDefinition("Sub Division", nameof(SubDivisionDesc), ColumnType.String),
                new ColumnDefinition("Department", nameof(DepartmentDesc), ColumnType.String)
            };
        }


        public void FillLookupDesc()
        {
            DirectorateDesc = Lookup.Directorate.GetText(DirectorateID);
            DivisionDesc = Lookup.Division.GetText(DivisionID);
            SubDivisionDesc = Lookup.SubDivision.GetText(SubDivisionID);
            DepartmentDesc = Lookup.Department.GetText(DepartmentID);
        }
    }


    public class ActSourceLookupList
    {
        public LookupList Directorate { get; set; }
        public LookupList Division { get; set; }
        public LookupList SubDivision { get; set; }
        public LookupList Department { get; set; }
    }






}
