//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Actris.Abstraction.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class MD_ActionTrackingSource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MD_ActionTrackingSource()
        {
            this.TX_ActionTracking = new HashSet<TX_ActionTracking>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TX_ActionTracking> TX_ActionTracking { get; set; }
    }
}
