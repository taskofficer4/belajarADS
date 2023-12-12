using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class ActReportDto : BaseDtoAutoMapper<TX_ActionTracking>
    {
        public string ActionTrackingID { get; set; }
        public string CorrectiveActionID { get; set; }
        public string ActionID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string ActionTrackingSource { get; set; }
        public string ReferenceID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string FindingDesc { get; set; }
        public string TypeShuRegion { get; set; }
        public string DirectorateRegionalDesc { get; set; }
        public string DivisiZonaDesc { get; set; }
        public string CompanyName { get; set; }
        public string WilayahkerjaDesc { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string Nct { get; set; }
        public string Rca { get; set; }
        public string CaID { get; set; }
        public string PendingAction { get; set; }
        public string StatusAction { get; set; }
        public string LastAction { get; set; }
        public Nullable<System.DateTime> LastActionDate { get; set; }
        public string PriorityTitle { get; set; }
        public string Recomendation { get; set; }
        public string ResponsibleDepartment { get; set; }
        public string ResponsibleManager { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> ProposedDueDate { get; set; }
        public string Justification { get; set; }
        public System.DateTime OverDueDate { get; set; }
        public Nullable<int> CumulativeOverdueDay { get; set; }
        public string OverdueReason { get; set; }
        public string OverdueImpact { get; set; }
        public string OverdueMitigation { get; set; }
        public string PlanCA { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string ActionTrackingReference { get; set; }
        public string AdditionalData { get; set; }
        public string SubdivisiDesc { get; set; }
        public string DepartementDesc { get; set; }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {

            return new List<ColumnDefinition>
            {
                new ColumnDefinition("ACT ID", nameof(ActionTrackingID), ColumnType.String, 190),
                new ColumnDefinition("ACT Source", nameof(ActionTrackingSource), ColumnType.String),
                new ColumnDefinition("Finding Description", nameof(FindingDesc), ColumnType.String),
                new ColumnDefinition("Issue Date", nameof(IssueDate), ColumnType.Date),

                //TODO: Location blm ada tabel nya
                new ColumnDefinition("Location", nameof(FindingDesc), ColumnType.String),
                new ColumnDefinition("Manager / Sr Manager Name", nameof(ResponsibleManager), ColumnType.String),
                new ColumnDefinition("Status", nameof(Status), ColumnType.String),
                new ColumnDefinition("Directorate / Regional", nameof(DirectorateRegionalDesc), ColumnType.String),
                new ColumnDefinition("Divisi / Zona", nameof(DivisiZonaDesc), ColumnType.String),
                new ColumnDefinition("Sub Division", nameof(SubdivisiDesc), ColumnType.String),
                new ColumnDefinition("Department", nameof(DepartementDesc), ColumnType.String),
                new ColumnDefinition("PIC 1", nameof(Pic1), ColumnType.String),
                new ColumnDefinition("PIC 2", nameof(Pic2), ColumnType.String),
                new ColumnDefinition("Creator", nameof(CreatedBy), ColumnType.String),
                new ColumnDefinition("Date", nameof(CreatedAt), ColumnType.Date),
            };
        }

        public static List<ColumnDefinition> GetExcelColumnDefinitions()
        {

            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Action ID", nameof(ActionID), ColumnType.String),
                new ColumnDefinition("Status", nameof(Status), ColumnType.String),
                new ColumnDefinition("Creator", nameof(CreatedBy), ColumnType.String),
                new ColumnDefinition("Created At", nameof(CreatedAt), ColumnType.Date),
                new ColumnDefinition("Issue Date", nameof(IssueDate), ColumnType.Date),

                new ColumnDefinition("Finding Description", nameof(FindingDesc), ColumnType.String),
                new ColumnDefinition("SHU / Region", nameof(TypeShuRegion), ColumnType.String),
                new ColumnDefinition("Directorate / Regional", nameof(DirectorateRegionalDesc), ColumnType.String),
                new ColumnDefinition("Divisi / Zona", nameof(DivisiZonaDesc), ColumnType.String),
                new ColumnDefinition("Company", nameof(CompanyName), ColumnType.String),
                new ColumnDefinition("Wilayah Kerja", nameof(WilayahkerjaDesc), ColumnType.String),
                new ColumnDefinition("Location", nameof(Location), ColumnType.String),
                new ColumnDefinition("Sub-Location", nameof(SubLocation), ColumnType.String),
                new ColumnDefinition("Not Comply to", nameof(Nct), ColumnType.String),
                new ColumnDefinition("Root Cause Analysis", nameof(Rca), ColumnType.String),

                new ColumnDefinition("CA ID", nameof(CaID), ColumnType.String),
                new ColumnDefinition("Pending Action", nameof(PendingAction), ColumnType.String),
                new ColumnDefinition("Action", nameof(StatusAction), ColumnType.String),

                new ColumnDefinition("Last Action", nameof(LastAction), ColumnType.String),
                new ColumnDefinition("Last Action Date", nameof(LastActionDate), ColumnType.Date),

                new ColumnDefinition("Priority Level", nameof(PriorityTitle), ColumnType.String),
                new ColumnDefinition("Recomendation", nameof(Recomendation), ColumnType.String),

            
                new ColumnDefinition("Responsible Department", nameof(ResponsibleDepartment), ColumnType.String),
                new ColumnDefinition("Manager", nameof(ResponsibleManager), ColumnType.String),
                new ColumnDefinition("PIC 1", nameof(Pic1), ColumnType.String),
                new ColumnDefinition("PIC 2", nameof(Pic2), ColumnType.String),
                new ColumnDefinition("DueDate", nameof(DueDate), ColumnType.Date),
                new ColumnDefinition("Proposed Due Date", nameof(ProposedDueDate), ColumnType.Date),
                new ColumnDefinition("Justification", nameof(Justification), ColumnType.String),
                new ColumnDefinition("Over Due Date", nameof(OverDueDate), ColumnType.Date),
                new ColumnDefinition("Cumulative Overdue Day", nameof(CumulativeOverdueDay), ColumnType.String),
                new ColumnDefinition("Reason", nameof(OverdueReason), ColumnType.String),
                new ColumnDefinition("Impact", nameof(OverdueImpact), ColumnType.String),
                new ColumnDefinition("Mitigation / Next Step", nameof(OverdueMitigation), ColumnType.String),
                new ColumnDefinition("Plan of Corrective Action", nameof(PlanCA), ColumnType.String),
                new ColumnDefinition("Date Completed", nameof(CompletionDate), ColumnType.Date)

            };
        }
    }
}
