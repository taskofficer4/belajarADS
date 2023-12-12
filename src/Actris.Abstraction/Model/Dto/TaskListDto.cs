using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
	public class TaskListDto : BaseDtoAutoMapper<TX_ActionTracking>
	{
		public bool IsApprove { get; set; }
		public bool IsReject { get; set; }
		public bool Remark { get; set; }
		public string ActionTrackingID { get; set; }
		public string ActionTrackingSource { get; set; }
		public string CorrectiveActionID { get; set; }
		public string Id => CorrectiveActionID;
		public string DirectorateRegionalDesc { get; set; }
		public string DivisiZonaDesc { get; set; }
		public string SubDivisionDesc { get; set; }
		public string DepartmentDesc { get; set; }
		public string WilayahKerjaDesc { get; set; }
		public string Location { get; set; }
		public string Recomendation { get; set; }
		public string Pic1EmpName { get; set; }
		public string Pic2EmpName { get; set; }
		public string CorrectiveActionPriority { get; set; }
		public DateTime? DueDate { get; set; }
		public string PendingAction { get; set; }
		public string Status { get; set; }

		public static List<ColumnDefinition> GetColumnDefinitions()
		{
			return new List<ColumnDefinition>
		 {
			 new ColumnDefinition("Id", nameof(Id), ColumnType.Id),
			new ColumnDefinition("Act ID", nameof(ActionTrackingID), ColumnType.String, 185),
			new ColumnDefinition("ACT Source", nameof(ActionTrackingSource), ColumnType.String),
			new ColumnDefinition("CA ID", nameof(CorrectiveActionID), ColumnType.String,185),
			new ColumnDefinition("DirectorateRegional", nameof(DirectorateRegionalDesc), ColumnType.String),
			new ColumnDefinition("DivisionZona", nameof(DivisiZonaDesc), ColumnType.String),
			new ColumnDefinition("SubDivision", nameof(SubDivisionDesc), ColumnType.String),
			new ColumnDefinition("Department", nameof(DepartmentDesc), ColumnType.String),
			new ColumnDefinition("WilayahKerja", nameof(WilayahKerjaDesc), ColumnType.String),
			new ColumnDefinition("Location", nameof(Location), ColumnType.String),
			new ColumnDefinition("Recomendation", nameof(Recomendation), ColumnType.String),
			new ColumnDefinition("PIC 1", nameof(Pic1EmpName), ColumnType.String),
			new ColumnDefinition("PIC 2", nameof(Pic2EmpName), ColumnType.String),
			new ColumnDefinition("PriorityLevel", nameof(CorrectiveActionPriority), ColumnType.String),
			new ColumnDefinition("DueDate", nameof(DueDate), ColumnType.Date),
			new ColumnDefinition("PendingAction", nameof(PendingAction), ColumnType.String),
			new ColumnDefinition("Status", nameof(Status), ColumnType.String),
		 };
		}


		public static List<ColumnDefinition> NeedApprovalGetColumnDefinitions()
		{

			var listColumns = new List<ColumnDefinition>
			 {
				new ColumnDefinition("Approve", nameof(IsApprove), ColumnType.InputCheckbox,89){DisableFilter=true},
				new ColumnDefinition("Reject", nameof(IsReject), ColumnType.InputCheckbox,68){DisableFilter=true},
				new ColumnDefinition("Remark", nameof(Remark), ColumnType.InputTextArea, 300){DisableFilter=true}
			 };

			listColumns.AddRange(GetColumnDefinitions());

			return listColumns;
		}

	}
}
