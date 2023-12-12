using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.Dto
{
   public class TrxMadam
   {
      public class DoTrx
      {
         public string AppId { get; set; }
         public string CompanyCode { get; set; }
         public string TransNo { get; set; }
         public string StartWF { get; set; }
         public string Action { get; set; }
         public string ActionFor { get; set; }
         public string ActionBy { get; set; }
         public string Source { get; set; }
         public string Notes { get; set; }
         public string AdditionalData { get; set; }
         public string TrxUserAuth { get; set; }

         public DoTrx()
         {

         }
      }

      public class Approver
      {
         public string Username { get; set; }
         public string Name { get; set; }
         public int Sequence { get; set; }
         public bool IsDone { get; set; }
         public object Action { get; set; }
         public List<AvailableAction> AvailableAction { get; set; }
      }

      public class AvailableAction
      {
         public string Name { get; set; }
         public string Code { get; set; }
      }

      public class AvailableFlow
      {
         public string SuccessWFCode { get; set; }
         public string SuccessWFName { get; set; }
         public string FailWFCode { get; set; }
         public string FailWFName { get; set; }
         public List<Approver> Approvers { get; set; }
      }

      public class Object
      {
         public string CurrentWFCode { get; set; }
         public string CurrentWFName { get; set; }
         public List<AvailableFlow> AvailableFlows { get; set; }
      }

      public class Root
      {
         public bool Status { get; set; }
         public Object Object { get; set; }
         public string Message { get; set; }
      }

      public class ResponseTrx
      {
         public string Key { get; set; }
         public string Value { get; set; }

      }
   }

}