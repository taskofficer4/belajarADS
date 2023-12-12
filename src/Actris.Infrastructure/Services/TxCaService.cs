using Actris.Abstraction.Enum;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Actris.Infrastructure.Services
{
    public class TxCaService : ITxCaService
    {
        private readonly ITxCaRepository _repo;
        private readonly ITxActService _actSvc;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ITxAttachmentRepository _attachmentRepo;
        private readonly IWorkflowService _workflowService;
        private readonly ITxActRepository _actRepo;
        //private readonly ITxCaService _caSvc;
        public TxCaService(ITxCaRepository repo, ITxAttachmentRepository attachmentRepo, IEmployeeRepository employeeRepo, ITxActService actSvc, IWorkflowService workflowService,ITxActRepository actRepo)//, ITxCaService caSvc)
        {
            _repo = repo;
            _attachmentRepo = attachmentRepo;
            _actSvc = actSvc;
            _employeeRepo = employeeRepo;
            _workflowService = workflowService;
            _actRepo = actRepo;
            //_caSvc = caSvc; 
        }

        public async Task SaveFollowUp(TxCaDto dto)
        {
            await _repo.SaveFollowUp(dto);
            var followUpAttachments = dto.Attachments.Where(o => o.ProjectPhase == ProjectPhaseEnum.FollowUp).ToList();
            await _attachmentRepo.Sync(ProjectPhaseEnum.FollowUp, dto.CorrectiveActionID, followUpAttachments);
            await SubmitFollowUp(dto);
        }

        public async Task SubmitFollowUp(TxCaDto dto)
        {
            var db = await GetOne(dto.CorrectiveActionID);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserResponsibleManager.CompanyCode,
                TransNo = db.TransID,
                Action = "SaveToMove",
                ActionBy = dto.ModifiedBy,
                ActionFor = dto.ModifiedBy,
                AdditionalData = db.UserResponsibleManager.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            db.FlowCode = flowCode.CurrentWFCode;
            db.FollowUpPlan = dto.FollowUpPlan;
            db.CompletionDate = dto.CompletionDate;
            db.ModifiedBy = dto.ModifiedBy;
            db.ModifiedDate = DateTime.Now;
            db.Status = CaStatusEnum.FollowUp;
            await _repo.SaveWorkflowResponse(db);

            //TODO: Submit ke workflow api
        }

        public async Task SubmitProposedDueDate(TxCaDto dto)
        {
            var projectPhase = ProjectPhaseEnum.Propose;
            await _repo.SaveProposedDueDate(dto);
            var followUpAttachments = dto.Attachments.Where(o => o.ProjectPhase == projectPhase).ToList();
            await _attachmentRepo.Sync(projectPhase, dto.CorrectiveActionID, followUpAttachments);

            //TODO: tempel api workflow
            var db = await GetOne(dto.CorrectiveActionID);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserPic1.CompanyCode,
                TransNo = db.TransID,
                Action = "Submit",
                ActionBy = db.ModifiedBy,
                ActionFor = db.ModifiedBy,
                Notes = dto.Remark,
                AdditionalData = db.UserResponsibleManager.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            dto.Status = flowCode.CurrentWFName;
            db.Status = CaStatusEnum.ProposeDueDate;
            db.FlowCode = flowCode.CurrentWFCode;
            db.TransID = dto.TransID;
            db.ModifiedBy = dto.ModifiedBy;
            db.ModifiedDate = DateHelper.WibNow;
            db.ModifiedBy= CurrentUser.GetPreferredUsername();
            db.ProposedDueDate = dto.ProposedDueDate;
            await _repo.SaveWorkflowResponse(db);

        }

        public async Task SubmitOverdue(TxCaDto dto)
        {
            var projectPhase = ProjectPhaseEnum.Overdue;
            await _repo.SaveOverdue(dto);
            var followUpAttachments = dto.Attachments.Where(o => o.ProjectPhase == projectPhase).ToList();
            await _attachmentRepo.Sync(projectPhase, dto.CorrectiveActionID, followUpAttachments);

            //TODO: tempel api workflow
            var db = await GetOne(dto.CorrectiveActionID);

            //Get VP dari Manager PosID
            var gm = await _employeeRepo.GetOneEmployeeByPosId(db.UserResponsibleManager.PosId);
            var vp = await _employeeRepo.GetExecutiveEmployee(gm.ParentPosId);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserPic1.CompanyCode,
                TransNo = db.TransID,
                Action = "Submit1",
                ActionBy = dto.ModifiedBy,
                ActionFor = dto.ModifiedBy,
                Notes = dto.OverdueReason,
                AdditionalData = vp.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            dto.Status = flowCode.CurrentWFName;
            db.Status = flowCode.CurrentWFName;
            db.FlowCode = flowCode.CurrentWFCode;
            db.TransID = dto.TransID;
            db.ModifiedBy = dto.ModifiedBy;
            db.ModifiedDate = DateHelper.WibNow;
            db.ProposedDueDate = dto.ProposedDueDate;
            await _repo.SaveWorkflowResponse(db);
        }

        public async Task<TxCaDto> GetOne(string caId)
        {
            var dto = await _repo.GetOne(caId);
            dto.Attachments = await _attachmentRepo.GetList(dto.CorrectiveActionID);
            dto.Act = await _actSvc.GetOneWithoutCa(dto.ActionTrackingID);
            return dto;
        }

        public void Update(TxCaDto dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task SubmitApproval(TxCaDto dto)
        {
            var db = await GetOne(dto.CorrectiveActionID);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserPic1.CompanyCode,
                TransNo = db.TransID,
                Action = dto.ApprovalAction,
                ActionBy = dto.ModifiedBy,// dto.ModifiedBy,
                ActionFor = dto.ModifiedBy,// dto.ModifiedBy,
                Notes = dto.Remark,
                AdditionalData = db.UserPic1.PosId            
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            dto.Status = flowCode.CurrentWFName;
            if(string.Equals(flowCode.CurrentWFCode,"10.3.2.1"))
                {
                db.Status = CaStatusEnum.ApproveProposeDueDate;
            }
            else if(string.Equals(flowCode.CurrentWFCode, "10.3.3.1"))
                {
                db.Status = CaStatusEnum.ApprovedForOverDueDate;
            }
            else if (string.Equals(flowCode.CurrentWFCode, "10.3.3.3"))
            {
                db.Status = CaStatusEnum.ApprovedForOverDueDateVP_GM;
            }
            else
            {
                db.Status = CaStatusEnum.ApprovedManager;
            }
            db.FlowCode = flowCode.CurrentWFCode;
            db.TransID = dto.TransID;
            db.ModifiedBy = dto.ModifiedBy;
            db.ModifiedDate = DateHelper.WibNow;
            await _repo.SaveWorkflowResponse(db);
        }

        public async Task SubmitForCompletion(TxCaDto dto)
        {
            var db = await GetOne(dto.CorrectiveActionID);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserPic1.CompanyCode,
                TransNo = db.TransID,
                Action = "Submit2",
                ActionBy = db.ResponsibleManagerUsername,
                ActionFor = db.ResponsibleManagerUsername,
                Notes = dto.Remark,
                AdditionalData = db.UserResponsibleManager.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            db.FlowCode = flowCode.CurrentWFCode;
            db.Status = CaStatusEnum.SubmitForCompletion;
            dto.Status = CaStatusEnum.SubmitForCompletion;
            await _repo.SaveWorkflowResponse(dto);
        }

        public async Task SubmitApprovalCompleted(TxCaDto dto)
        {
            var db = await GetOne(dto.CorrectiveActionID);
            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserResponsibleManager.CompanyCode,
                TransNo = db.TransID,
                Action = dto.ApprovalAction,
                ActionBy = dto.ModifiedBy,
                ActionFor = dto.ModifiedBy,
                Notes = dto.Remark,
                AdditionalData = db.UserResponsibleManager.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            dto.Status = flowCode.CurrentWFName;
            if (flowCode.CurrentWFCode == "10.3.4.1")
                dto.Status = CaStatusEnum.ApprovedManager;
            else
                dto.Status = "Reject";
                //string.Equals(dto.ApprovalAction, "approve", StringComparison.OrdinalIgnoreCase) ? "Closed" : "Open";
            await _repo.SaveWorkflowResponse(dto);
            
            //closed status Act
            var dtoAct = await _actSvc.GetOne(db.ActionTrackingID);
            if (dtoAct != null)
            {
                dtoAct.Status = ActStatusEnum.Closed;
                if (dto.Status == CaStatusEnum.ApprovedManager)
                    dtoAct.Status = ActStatusEnum.Closed;
                else
                    dtoAct.Status = ActStatusEnum.Open;
                await _actRepo.SaveWorkflowResponse(dtoAct);
            }
            
            //throw new System.NotImplementedException();
        }

        public async Task SubmitApprovalDueDate(TxCaDto dto)
        {
            var db = await GetOne(dto.CorrectiveActionID);

            var param = new TrxMadam.DoTrx
            {
                CompanyCode = db.UserPic1.CompanyCode,
                TransNo = db.TransID,
                Action = dto.ApprovalAction,
                ActionBy = db.UserPic1.PosId,
                ActionFor = db.UserPic1.PosId,
                Notes = dto.Remark,
                AdditionalData = db.UserPic1.PosId
            };
            TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
            dto.TransID = response.Value;
            var flowCode = await _workflowService.GetMadamStatusInfo(response.Value);
            dto.FlowCode = flowCode.CurrentWFCode;
            db.FlowCode = flowCode.CurrentWFCode;
            db.Status = flowCode.CurrentWFName;
            dto.Status = CaStatusEnum.ProposeDueDate;
            dto.ModifiedBy = CurrentUser.GetPreferredUsername();
            dto.ModifiedDate = DateHelper.WibNow;
            await _repo.SaveWorkflowResponse(dto);
        }
    }
}
