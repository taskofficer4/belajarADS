namespace Actris.Abstraction.Model.DbView
{
   public class VwEmployeeLs
   {
      public string id => EmpID;
      public string EmpID { get; set; }
      public string EmpName { get; set; }
      public string DepartmentDesc { get; set; }
      public string DivisionDesc { get; set; }
      public string EmpSubGroupName { get; set; }
      public string PositionTitle { get; set; }
      public string PosID { get; set; }
      public string ParentPosID { get; set; }

      public string DisplayText()
      {
         if (!string.IsNullOrEmpty(DepartmentDesc))
         {
            return $"{DepartmentDesc} - {EmpName}";
         }
         return EmpName;
      }

      public string DisplayWithPosition()
      {
         if (!string.IsNullOrEmpty(PositionTitle))
         {
            return $"{PositionTitle} - {EmpName}";
         }
         return EmpName;
      }
   }
}
