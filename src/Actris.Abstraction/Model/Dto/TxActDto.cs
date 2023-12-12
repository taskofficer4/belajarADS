using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Actris.Abstraction.Model.Dto
{
   public class TxActDto : BaseDtoAutoMapper<TX_ActionTracking>
   {
      public string ActionTrackingID { get; set; }
      public string ActionTrackingReference { get; set; }
      public string LocationStatusID { get; set; }
      public string ObservationID { get; set; }
      public string ActionTrackingSourceKey { get; set; }
      public string ActionTrackingSource { get; set; }
      public string SourceDirectorateID { get; set; }
      public string SourceDivisionID { get; set; }
      public string SourceSubDivisionID { get; set; }
      public string SourceDepartmentID { get; set; }
      public Nullable<System.DateTime> IssueDate { get; set; }
      public string FindingDesc { get; set; }
      public string LocationID { get; set; }
      public string Location { get; set; }
      public string SubLocation { get; set; }
      public string Nct { get; set; }
      public string Rca { get; set; }
      public string Status { get; set; }
      public string DataStatus { get; set; }
      public string AdditionalData { get; set; }
      public string TypeShuRegion { get; set; }
      public string DirectorateRegionalID { get; set; }
      public string DirectorateRegionalDesc { get; set; }
      public string DivisiZonaID { get; set; }
      public string DivisiZonaDesc { get; set; }
      public string CompanyCode { get; set; }
      public string CompanyName { get; set; }
      public string WilayahkerjaID { get; set; }
      public string WilayahkerjaDesc { get; set; }
      public bool IsConfidential { get; set; }
      public string CreatedBy { get; set; }
      public Nullable<System.DateTime> CreatedDate { get; set; }
      public string ModifiedBy { get; set; }
      public Nullable<System.DateTime> ModifiedDate { get; set; }

      public ActionTrackingLookup Lookup { get; set; }

      public List<FileAttachmentDto> Attachments { get; set; }
      public List<TxCaDto> CaList { get; set; }

      //field pendukung
      public bool IsSubmit { get; set; }

      public FormState FormState { get; set; }



      public TxActDto()
      {
         Lookup = new ActionTrackingLookup();
         Attachments = new List<FileAttachmentDto>();
         CaList = new List<TxCaDto>();
      }

      public TxActDto(TX_ActionTracking entity)
      {
         Mapper.Map(entity, this);
         Lookup = new ActionTrackingLookup();
         Attachments = new List<FileAttachmentDto>();
      }

      public static List<ColumnDefinition> GetColumnDefinitions()
      {
         return new List<ColumnDefinition>
            {
                new ColumnDefinition("Act ID", nameof(ActionTrackingID), ColumnType.String),
                new ColumnDefinition("ACT Source", nameof(ActionTrackingSource), ColumnType.String),
                new ColumnDefinition("Reference ID", nameof(AdditionalData), ColumnType.String),
                new ColumnDefinition("Finding Description", nameof(FindingDesc), ColumnType.String),
                new ColumnDefinition("Issue Date", nameof(IssueDate), ColumnType.Date),
                new ColumnDefinition("Location", nameof(Location), ColumnType.String),
                new ColumnDefinition("Status", nameof(Status), ColumnType.String),
                new ColumnDefinition("Creator", nameof(CreatedBy), ColumnType.String),
                new ColumnDefinition("Date", nameof(CreatedDate), ColumnType.Date)
            };
      }

      /// <summary>
      /// Isi dari ID dropdown ke field description (text)
      /// </summary>
      public void FillLookupDesc()
      {
         DirectorateRegionalDesc = Lookup.DirectorateRegional.GetText(DirectorateRegionalID);
         DivisiZonaDesc = Lookup.DivisionZona.GetText(DivisiZonaID);
         CompanyName = Lookup.Company.GetText(CompanyCode);
         WilayahkerjaDesc = Lookup.WilayahKerja.GetText(WilayahkerjaID);
         Location = Lookup.LocationCompany.GetText(LocationID);


         // SET ACT SOURCE 
         var actSource = new MdActSourceDto();
         actSource.FromKeyString(ActionTrackingSourceKey);

         ActionTrackingSource = actSource.Source;
         SourceDepartmentID = actSource.DepartmentID;
         SourceDirectorateID = actSource.DirectorateID;
         SourceDivisionID = actSource.DivisionID;
         SourceSubDivisionID = actSource.SubDivisionID;
      }
   }

   public class ActionTrackingLookup
   {
      public LookupList ActSource { get; set; }
      public LookupList DirectorateRegional { get; set; }
      public LookupList DivisionZona { get; set; }
      public LookupList Company { get; set; }
      public LookupList WilayahKerja { get; set; }
      public LookupList LocationCompany { get; set; }
      public LookupList LocationStatus { get; set; }
   }
}
