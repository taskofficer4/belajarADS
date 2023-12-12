using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Model.Response
{
	public class ApprovalResult
	{
		public string CorrectiveActionID { get; set; }
		public string ApprovalAction { get; set; }
		public bool Success { get; set; }
		public string TransID { get; set; }
		public string ErrorMessage { get; set; }
		public ApprovalResult()
		{

		}

		public ApprovalResult(TxCaDto dto)
		{
			CorrectiveActionID = dto.CorrectiveActionID;
			TransID = dto.TransID;
			ApprovalAction= dto.ApprovalAction;
		}
	}
}
