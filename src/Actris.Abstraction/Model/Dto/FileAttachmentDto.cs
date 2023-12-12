using Actris.Abstraction.Model.Entities;
using AutoMapper;
using DocumentFormat.OpenXml.AdditionalCharacteristics;
using System;

namespace Actris.Abstraction.Model.Dto
{
   public class FileAttachmentDto
   {
      public string ReferenceCode { get; set; }
      public string ProjectPhase { get; set; }
      public string DocumentType { get; set; }
      public string FilePath { get; set; }
      public byte[] File { get; set; }
      public string FileName { get; set; }
      public string FileType { get; set; }
      public Nullable<decimal> FileSize { get; set; }
      public Nullable<System.DateTime> CreatedDate { get; set; }
      public string CreatedBy { get; set; }
      public Nullable<System.DateTime> UpdatedDate { get; set; }
      public string UpdatedBy { get; set; }
      public string FileDescription { get; set; }

      // ADDITIONAL FIELD
      public string FolderToUpload { get; set; }
      public string FileUrl { get; set; }

      public FileAttachmentDto()
      {

      }


      public FileAttachmentDto(TX_Attachment entity)
      {
         Mapper.Map(entity, this);
      }
      public TX_Attachment ToEntity()
      {
         var entity = Mapper.Map<TX_Attachment>(this);
         return entity;
      }
   }
}
