using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class ActionTrackingViewDto
    {
        public string ActId { get; set; }
        public string ActSource { get; set; }
        public string ReferenceId { get; set; }
        public string IssueDate { get; set; }
        public string FindingDescription { get; set; }
        public string TypeShuRegion{ get; set; }
        public string DirectorateRegional { get; set; }
        public string DivisionZona { get; set; }
        public string Company { get; set; }
        public string WilayahKerja { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string LocationStatus { get; set; }
        public string Nct { get; set; }
        public string Rca { get; set; }
        public List<string> Attachments { get; set; }

        public List<CaListViewDto> ListAction { get; set; }

    }

    public class CaListViewDto
    {
        public bool IsApprove { get; set; }
        public bool IsReject { get; set; }
        public string Remarks { get; set; }
        public string CaId { get; set; }
        public string Department { get; set; }
        public string Recomendation { get; set; }
        public List<string> ListPic { get; set; }
        public string PriorityLevel { get; set; }
        public string PendingAction { get; set; }
        public string Status { get; set; }
    }
}
