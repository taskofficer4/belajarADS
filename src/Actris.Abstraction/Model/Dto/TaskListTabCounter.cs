namespace Actris.Abstraction.Model.Dto
{
   public class TaskListTabCounter
   {
      public bool AllowNeedAction { get; set; }
      public bool AllowNeedApproval { get; set; }
      public bool AllowOverdue { get; set; }

      public string CurrentTab { get; set; }

      public int NeedAction { get; set; }
      public int NeedApproval { get; set; }
      public int Overdue { get; set; }
   }
}
