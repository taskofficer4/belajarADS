using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
   public class TxCaDto : BaseDtoAutoMapper<TX_CorrectiveAction>
   {
      public string Id { get; set; }
      public string CorrectiveActionID { get; set; }
      public string ActionTrackingID { get; set; }
      public string CorrectiveActionPriority { get; set; }
      public string ResponsibleDivision { get; set; }
      public string ResponsibleDivisionID { get; set; }
      public string ResponsibleDepartment { get; set; }
      public string ResponsibleDepartmentID { get; set; }
      public string ResponsibleManager { get; set; }
      public string ResponsibleManagerApprover { get; set; }
      public string ResponsibleManagerUsername { get; set; }
      public string Recomendation { get; set; }
      public Nullable<System.DateTime> DueDate { get; set; }
      public Nullable<System.DateTime> ProposedDueDate { get; set; }
      public string ProposedDueDateApprover { get; set; }
      public string ProposedDueDateData { get; set; }
      public Nullable<System.DateTime> CompletionDate { get; set; }
      public string OverdueReason { get; set; }
      public string OverdueImpact { get; set; }
      public string OverdueMitigation { get; set; }
      public string OverdueStatus { get; set; }
      public string FollowUpPlan { get; set; }
      public string AdditionalData { get; set; }
      public string Status { get; set; }
      public string DataStatus { get; set; }
      public string FlowCode { get; set; }
      public string TransID { get; set; }
      public string CreatedBy { get; set; }
      public Nullable<System.DateTime> CreatedDate { get; set; }
      public string ModifiedBy { get; set; }
      public Nullable<System.DateTime> ModifiedDate { get; set; }

      public List<TxCaUserDto> UserList { get; set; }



      #region FieldPendukung
      public string ResponsibleManagerEmpName { get; set; }
      public string ResponsibleManagerView => $"{ResponsibleDepartment} - {ResponsibleManagerEmpName}";
      public int Index { get; set; }
      public CaLookup Lookup { get; set; }
      public FormState State { get; set; }

      public string Pic1 { get; set; }
      public string Pic1EmpName { get; set; }
      public string Pic2 { get; set; }
      public string Pic2EmpName { get; set; }

      public TxCaUserDto UserPic1 { get; set; }
      public TxCaUserDto UserPic2 { get; set; }
      public TxCaUserDto UserResponsibleManager { get; set; }
      public string DisplayPic
      {
         get
         {
            var mergePic = Pic1EmpName;
            if (Pic2EmpName != null) mergePic += $", {Pic2EmpName}";
            return mergePic;
         }
      }
      public bool IsApprove { get; set; }
      public bool IsReject { get; set; }
      public bool PendingAction { get; set; }
      public string Remark { get; set; }

      public List<FileAttachmentDto> Attachments { get; set; }

      public TxActDto Act { get; set; }
      public bool IsSubmit { get; set; }

      public string ApprovalAction { get; set; }

      #endregion
      public TxCaDto()
      {
         Lookup = new CaLookup();
         UserList = new List<TxCaUserDto>();
         Attachments = new List<FileAttachmentDto>();
      
      }

      public TxCaDto(TX_CorrectiveAction entity) : base(entity)
      {
         Attachments = new List<FileAttachmentDto>();

         UserList = new List<TxCaUserDto>();
         Lookup = new CaLookup();
      }

      public static List<ColumnDefinition> GetColumnDefinitions()
      {
         return new List<ColumnDefinition>
         {
            //new ColumnDefinition("Id", nameof(CaPriorityDto.CorrectiveActionPriorityID), ColumnType.String),
            //new ColumnDefinition("Title (en)", nameof(CaPriorityDto.PriorityTitle), ColumnType.String),
            //new ColumnDefinition("Value (en)", nameof(CaPriorityDto.PriorityValue), ColumnType.String),
            //new ColumnDefinition("Title (id)", nameof(CaPriorityDto.PriorityTitle2), ColumnType.String),
            //new ColumnDefinition("Value (id)", nameof(CaPriorityDto.PriorityValue2), ColumnType.String),
            //new ColumnDefinition("Duration", nameof(CaPriorityDto.PriorityDuration), ColumnType.Number)
         };
      }
   }

   public class CaLookup
   {
      public LookupList CaPriority { get; set; }
      public LookupList ResponsibleManager { get; set; }
      public LookupList Pic1 { get; set; }
      public LookupList Pic2 { get; set; }

      public LookupList OverdueImpact { get; set; }
   }
}
