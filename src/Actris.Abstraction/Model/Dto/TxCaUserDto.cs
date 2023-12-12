using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Actris.Abstraction.Model.Dto
{
   public class TxCaUserDto : BaseDtoAutoMapper<TX_CorrectiveActionUser>
   {
      public string CorrectiveActionID { get; set; }
      public string EmpAccount { get; set; }
      public string Role { get; set; }
      public string EmpEmail { get; set; }
      public string EmpName { get; set; }
      public string EmpId { get; set; }
      public string CompanyCode { get; set; }
      public string CompanyName { get; set; }
      public string DirectorateId { get; set; }
      public string DirectorateDesc { get; set; }
      public string DivisionId { get; set; }
      public string DivisionDesc { get; set; }
      public string DepartmentId { get; set; }
      public string DepartmentDesc { get; set; }
      public string PosId { get; set; }
      public string PosTitle { get; set; }
      public string ParentPosId { get; set; }
      public string ParentPosTitle { get; set; }
      public string SectionId { get; set; }
      public string SectionDesc { get; set; }
      public string SubDivisionId { get; set; }
      public string SubDivisionDesc { get; set; }
      public string UserType { get; set; }
      public string DataStatus { get; set; }
      public string CreatedBy { get; set; }
      public Nullable<System.DateTime> CreatedDate { get; set; }
      public string ModifiedBy { get; set; }
      public Nullable<System.DateTime> ModifiedDate { get; set; }

      public TxCaUserDto(TX_CorrectiveActionUser entity) : base(entity)
      {
      }

      public TxCaUserDto()
      {

      }
   }
}
