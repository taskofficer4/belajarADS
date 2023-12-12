using Actris.Abstraction.Enum;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Helper;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Services
{
    public class TxActService : BaseCrudService<TxActDto, TxActDto>, ITxActService
    {
        private readonly ITxActRepository _repo;
        private readonly ITxCaRepository _caRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ITxAttachmentRepository _attachmentRepo;
        private readonly IWorkflowService _workflowService;
        public TxActService(ITxActRepository repo, ITxAttachmentRepository attachmentRepo, ITxCaRepository txCaRepository, IEmployeeRepository employeeRepo, IWorkflowService workflowService) : base(repo)
        {
            _repo = repo;
            _attachmentRepo = attachmentRepo;
            _caRepo = txCaRepository;
            _employeeRepo = employeeRepo;
            _workflowService = workflowService;
        }

        public override async Task Create(TxActDto dto)
        {
            // 1. Fill User detail from EmpID (Manager, Pic1, Pic2)
            await PopulateUserFromEmpId(dto);

            // 2. create act (header)
            await _repo.Create(dto);

            // 3. add or delete act attachment
            await _attachmentRepo.Sync(ProjectPhaseEnum.Initiator, dto.ActionTrackingID, dto.Attachments);

            // 4. hit api workflow
            await SubmitApiWorkflow(dto);

            // 5. add or delete ca list
            await _caRepo.Sync(dto.ActionTrackingID, dto.CaList, dto.IsSubmit);

            // 6. add or delete ca attachment
            foreach (var ca in dto.CaList)
            {
                await _attachmentRepo.Sync(ProjectPhaseEnum.Initiator, ca.CorrectiveActionID, ca.Attachments);
            }
        }

        private async Task SubmitApiWorkflow(TxActDto dto)
        {

            dto.CreatedBy = CurrentUser.GetPreferredUsername();
            if (dto.IsSubmit)
            {
                var param = new TrxMadam.DoTrx
                {
                    CompanyCode = dto.CompanyCode,
                    ActionBy = dto.CreatedBy,
                    ActionFor = dto.CreatedBy,
                    AdditionalData = ""
                };


                foreach (var ca in dto.CaList)
                {
                    
                    TrxMadam.ResponseTrx response = await _workflowService.DoMadamTrx(param);
                    ca.TransID = response.Value;
                    var flowCode = await _workflowService.GetMadamStatusInfo(ca.TransID);
                    ca.FlowCode = flowCode.CurrentWFCode;
                    param.TransNo = response.Value;
                    ca.CreatedBy = dto.CreatedBy;
                    await _caRepo.SaveWorkflowResponse(ca);
                    param.AdditionalData = ca.UserList[0].PosId;
                    param.Action = "SaveToMove";
                    TrxMadam.ResponseTrx responseSubmit = await _workflowService.DoMadamTrx(param);
                    flowCode = await _workflowService.GetMadamStatusInfo(ca.TransID);
                    ca.FlowCode = flowCode.CurrentWFCode;
                    ca.Status = CaStatusEnum.Submitted;
                    await _caRepo.SaveWorkflowResponse(ca);
                }               
            }
            else
            {
            //    var param = new TrxMadam.DoTrx
            //    {
            //        CompanyCode = dto.CompanyCode,
            //        ActionBy = dto.CreatedBy,
            //        ActionFor = dto.CreatedBy,
            //        AdditionalData = ""
            //    };

            //    foreach (var ca in dto.CaList)
            //    {
            //        var db = await _caRepo.GetOne(ca.CorrectiveActionID);
            //        db.TransID = ca.TransID;
            //    }
            }
        }

        private async Task PopulateUserFromEmpId(TxActDto dto)
        {
            foreach (var ca in dto.CaList)
            {
                ca.UserList = new List<TxCaUserDto>();

                // get user detail manager
                if (!string.IsNullOrEmpty(ca.ResponsibleManager))
                {
                    var manager = await _employeeRepo.GetOneEmployee(ca.ResponsibleManager);
                    ca.ResponsibleDepartment = manager.DepartmentDesc;
                    ca.ResponsibleDepartmentID = manager.DepartmentId;

                    ca.ResponsibleDivision = manager.DivisionDesc;
                    ca.ResponsibleDivisionID = manager.DivisionId;

                    ca.ResponsibleManagerUsername = manager.EmpAccount;
                    manager.Role = "ResponsibleManager";
                    ca.UserList.Add(manager);
                }

                // get user detail pic1
                if (!string.IsNullOrEmpty(ca.Pic1))
                {
                    var pic1 = await _employeeRepo.GetOneEmployee(ca.Pic1) == null ?  await _employeeRepo.GetOneEmployeeByPosId(ca.Pic1) : await _employeeRepo.GetOneEmployee(ca.Pic1);
                    pic1.Role = "PIC1";
                    ca.UserList.Add(pic1);
                }

                // get user detail pic2
                if (!string.IsNullOrEmpty(ca.Pic2))
                {
                    var pic2 = await _employeeRepo.GetOneEmployee(ca.Pic2) == null ? await _employeeRepo.GetOneEmployeeByPosId(ca.Pic2) : await _employeeRepo.GetOneEmployee(ca.Pic2);
                    pic2.Role = "PIC2";
                    ca.UserList.Add(pic2);
                }
            }
        }

        public override async Task Update(TxActDto dto)
        {
            // 0. Fill User detail from EmpID (Manager, Pic1, Pic2)
            await PopulateUserFromEmpId(dto);

            // 1. update act (header)
            await _repo.Update(dto);

            // 2. add or delete act attachment
            await _attachmentRepo.Sync(ProjectPhaseEnum.Initiator, dto.ActionTrackingID, dto.Attachments);

            await SubmitApiWorkflow(dto);

            // 3. add or delete ca list
            await _caRepo.Sync(dto.ActionTrackingID, dto.CaList, dto.IsSubmit);

            // 4. add or delete ca attachment
            foreach (var ca in dto.CaList)
            {
                await _attachmentRepo.Sync(ProjectPhaseEnum.Initiator, ca.CorrectiveActionID, ca.Attachments);
            }

            // TODO: 5. SUBMIT KE WORK FLOW
            if (dto.IsSubmit)
            {

            }
        }

        public override async Task<TxActDto> GetOne(string id)
        {
            TxActDto dto = await base.GetOne(id);
            dto.ActionTrackingSourceKey = ActSourceKeyHelper.ToKeyString(dto.ActionTrackingSource, dto.SourceDirectorateID, dto.SourceDivisionID, dto.SourceSubDivisionID, dto.SourceDepartmentID);
            dto.Attachments = await _attachmentRepo.GetList(id);
            dto.CaList = await _caRepo.GetList(id);

            var i = 0;
            foreach (var ca in dto.CaList)
            {
                ca.Index = i++;
                ca.Attachments = await _attachmentRepo.GetList(ca.CorrectiveActionID);
            }
            return dto;
        }

        public async Task<TxActDto> GetOneWithoutCa(string id)
        {
            TxActDto dto = await base.GetOne(id);
            dto.ActionTrackingSourceKey = ActSourceKeyHelper.ToKeyString(dto.ActionTrackingSource, dto.SourceDirectorateID, dto.SourceDivisionID, dto.SourceSubDivisionID, dto.SourceDepartmentID);
            dto.Attachments = await _attachmentRepo.GetList(id);
            return dto;
        }

        public List<TxCaDto> GetCaList(string actRef)
        {
            return _repo.GetCaList(actRef);
        }
    }
}
