namespace Actris.Abstraction.Enum
{
	public static class CaStatusEnum
	{
		/// <summary>
		/// When Corrective Action hasn't been submitted to next role of action 
		/// </summary>
		public const string Draft = "Draft";

		/// <summary>
		/// When Corrective Action has been submitted 
		/// </summary>
		public const string Submitted = "Submitted";

		/// <summary>
		/// When Manager has approved related Corrective Action 
		/// </summary>
		public const string ApprovedManager = "Approved-Manager";

		/// <summary>
		/// When PIC has submitted follow up
		/// </summary>
		public const string FollowUp = "Follow-Up";

		/// <summary>
		/// When PIC has submitted and requested for completion
		/// </summary>
		public const string SubmitForCompletion = "Submit for Completion";

		/// <summary>
		/// When PIC requests for new due date
		/// </summary>
		public const string ProposeDueDate = "Propose Due Date";

		/// <summary>
		/// When Manager has approved proposed due date by PIC
		/// </summary>
		public const string ApproveProposeDueDate = "Approve Propose Due Date";

		/// <summary>
		/// When SLA has reached over due and PIC requests for over due submission
		/// </summary>
		public const string RequestOverDue = "Request Over Due";

		/// <summary>
		/// When Manager has approved the request for over due date by PIC
		/// </summary>
		public const string ApprovedForOverDueDate = "Approved for Over Due Date";

		/// <summary>
		/// When Manager has approved the request for over due date by PIC
		/// </summary>
		public const string ApprovedForOverDueDateVP_GM = "Approved For Over Due Date VP/GM";
	}
}