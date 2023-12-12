using DocumentFormat.OpenXml.Drawing.Charts;
using System.Collections.Generic;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Model.Excel
{
    public class ProjectInformationExcel
    {
        public string TransactionID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string UBDActivityTypeParName { get; set; }
        public string TransactionTypeParName { get; set; }
        public string Seller { get; set; }
        public string BusinessVentureParName { get; set; }
        public string FollowUpPlanParName { get; set; }
        public string PersoninChargeEmpName { get; set; }
        public string ProjectLeaderEmpName { get; set; }
        public string ProjectSecretaryEmpName { get; set; }
        public string ProjectSummary { get; set; }
        public string ProjectSuspendedNotes { get; set; }
        public string ProjectClosingNotes { get; set; }
        public string ProjectClosingReport { get; set; }
        public string ProjectStatusParName { get; set; }

        public ProjectInformationExcel() { }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Transaction ID", nameof(TransactionID), ColumnType.String),
                new ColumnDefinition("Project Name", nameof(ProjectName), ColumnType.String),
                new ColumnDefinition("Project Description", nameof(ProjectDescription), ColumnType.RichText),
                new ColumnDefinition("Activity Type", nameof(UBDActivityTypeParName), ColumnType.String),
                new ColumnDefinition("Transaction Type", nameof(TransactionTypeParName), ColumnType.String),
                new ColumnDefinition("Seller", nameof(Seller), ColumnType.String),
                new ColumnDefinition("Business Venture", nameof(BusinessVentureParName), ColumnType.String),
                new ColumnDefinition("FollowUp Plan", nameof(FollowUpPlanParName), ColumnType.String),
                new ColumnDefinition("Person in Charge", nameof(PersoninChargeEmpName), ColumnType.String),
                new ColumnDefinition("Project Leader", nameof(ProjectLeaderEmpName), ColumnType.String),
                new ColumnDefinition("Project Secretary", nameof(ProjectSecretaryEmpName), ColumnType.String),
                new ColumnDefinition("Project Summary", nameof(ProjectSummary), ColumnType.RichText),
                new ColumnDefinition("Project Suspended Notes", nameof(ProjectSuspendedNotes), ColumnType.String),
                new ColumnDefinition("Project Closing Notes", nameof(ProjectClosingNotes), ColumnType.String),
                new ColumnDefinition("Project Closing Report", nameof(ProjectClosingReport), ColumnType.String),
                new ColumnDefinition("Project Status", nameof(ProjectStatusParName), ColumnType.String),
            };
        }
    }
}
