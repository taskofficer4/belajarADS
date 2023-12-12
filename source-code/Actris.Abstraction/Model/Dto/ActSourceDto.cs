using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Web.Util;

namespace Actris.Abstraction.Model.Dto
{
    public class ActSourceDto : BaseDtoAutoMapper<MD_ActionTrackingSource>
    {
        public string ActionTrackingSourceID { get; set; }
        public string SourceTitle { get; set; }
        public string SourceValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string DataStatus { get; set; }
        public string SourceTitle2 { get; set; }
        public string SourceValue2 { get; set; }
        public string DirectorateRegionalID { get; set; }
        public string DirectorateRegionalDesc { get; set; }
        public string DivisiZonaID { get; set; }
        public string DivisiZonaDesc { get; set; }
        public string WilayahkerjaID { get; set; }
        public string WilayahkerjaDesc { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentDesc { get; set; }
        public string DivisionID { get; set; }
        public string DivisionDesc { get; set; }
        public string SubDivisionID { get; set; }
        public string SubDivisionDesc { get; set; }
        public string FunctionID { get; set; }
        public string FunctionDesc { get; set; }


        public ActSourceLookupList Lookup { get; set; }


        public ActSourceDto()
        {
        }

        public ActSourceDto(MD_ActionTrackingSource entity) : base(entity)
        {
        }
        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", nameof(ActionTrackingSourceID), ColumnType.String),

                new ColumnDefinition("Title (en)", nameof(SourceTitle), ColumnType.String),
                new ColumnDefinition("Value (en)", nameof(SourceValue), ColumnType.String),
                new ColumnDefinition("Title (id)", nameof(SourceTitle2), ColumnType.String),
                new ColumnDefinition("Value (id)", nameof(SourceValue2), ColumnType.String),

                new ColumnDefinition("Directorat Regional", nameof(DirectorateRegionalDesc), ColumnType.String),
                new ColumnDefinition("Divisi Zona", nameof(DivisiZonaDesc), ColumnType.String),
                new ColumnDefinition("Wilayah Kerja", nameof(WilayahkerjaDesc), ColumnType.String),
                new ColumnDefinition("Department", nameof(DepartmentDesc), ColumnType.String),
                new ColumnDefinition("Division", nameof(DivisionDesc), ColumnType.String),
                new ColumnDefinition("Sub Division", nameof(SubDivisionDesc), ColumnType.String),
                new ColumnDefinition("Function", nameof(FunctionDesc), ColumnType.String)
            };
        }


        public void FillLookupDesc()
        {
            DirectorateRegionalDesc = Lookup.DirectorateRegional.GetText(DirectorateRegionalID);
            DivisiZonaDesc = Lookup.DirectorateRegional.GetText(DivisiZonaID);
            WilayahkerjaDesc = Lookup.DirectorateRegional.GetText(WilayahkerjaID);
            DepartmentDesc = Lookup.DirectorateRegional.GetText(DepartmentID);
            DivisionDesc = Lookup.DirectorateRegional.GetText(DivisionID);
            SubDivisionDesc = Lookup.DirectorateRegional.GetText(SubDivisionID);
            FunctionDesc = Lookup.DirectorateRegional.GetText(FunctionID);
        }
    }


    public class ActSourceLookupList
    {
        public LookupList DirectorateRegional { get; set; }
        public LookupList DivisiZona { get; set; }
        public LookupList WilayahKerja { get; set; }
        public LookupList Department { get; set; }
        public LookupList Division { get; set; }
        public LookupList SubDivision { get; set; }
        public LookupList Function { get; set; }
    }






}
