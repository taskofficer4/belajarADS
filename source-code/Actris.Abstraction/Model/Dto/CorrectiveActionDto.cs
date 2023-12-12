using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class CorrectiveActionDto : BaseDtoAutoMapper<TX_CorrectiveAction>
    {
        public string CorrectiveActionID { get; set; }
        public string CorrectiveActionOverdueImpactID { get; set; }
        public string ActionTrackingID { get; set; }
        public string CorrectiveActionPriorityID { get; set; }
        public string CorrectiveActionReference { get; set; }
        public string ResponsibleDivision { get; set; }
        public string ResponsibleDivisionID { get; set; }
        public string ResponsibleDepartment { get; set; }
        public string ResponsibleDepartmentID { get; set; }
        public string ResponsibleManager { get; set; }
        public string ResponsibleManagerApprover { get; set; }
        public string ResponsibleManagerUsername { get; set; }
        public string ResponsibleDepartmentData { get; set; }
        public string Recomendation { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> ProposedDueDate { get; set; }
        public string ProposedDueDateApprover { get; set; }
        public string ProposedDueDateData { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string PicData { get; set; }

        public string Pic1 { get; set; }
        public string AttachmentData { get; set; }
        public string FollowUpData { get; set; }
        public string OverdueData { get; set; }
        public string AdditionalData { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public string Status { get; set; }
        public string DataStatus { get; set; }
        public string FlowCode { get; set; }
        public string TransID { get; set; }

        public virtual MD_CorrectiveActionOverdueImpact MD_CorrectiveActionOverdueImpact { get; set; }
        public virtual MD_CorrectiveActionPriority MD_CorrectiveActionPriority { get; set; }
        public virtual TX_ActionTracking TX_ActionTracking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TX_CorrectiveActionHistory> TX_CorrectiveActionHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TX_CorrectiveActionUser> TX_CorrectiveActionUser { get; set; }

        public CorrectiveActionDto()
        {
        }

        public CorrectiveActionDto(TX_CorrectiveAction entity) : base(entity)
        {
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
}
