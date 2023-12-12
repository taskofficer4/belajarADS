using Actris.Abstraction.Enum;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Extensions;
using Actris.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
	public class TxCaRepository :
		BaseCrudRepository<TX_CorrectiveAction,
			TxCaDto,
			TxCaDto,
			TxActionTrackingQuery>,
		ITxCaRepository
	{
		private static object _lock = new object();
		private static object _lockSubmit = new object();
		public TxCaRepository(ActrisContext context, IConnectionProvider connection, TxActionTrackingQuery tCrudQuery) : base(context, connection, tCrudQuery)
		{
		}

		public override async Task Create(TxCaDto dto)
		{
			if (_context.TX_CorrectiveAction.Any(o => o.CorrectiveActionID == dto.CorrectiveActionID))
			{
				return;
			}
			lock (_lock)
			{
				dto.CorrectiveActionID = GenerateCaId();
				var ca = dto.ToEntity();
				if (dto.IsSubmit)
				{
					ca.Status = CaStatusEnum.Submitted;
				}
				else
				{
					ca.Status = CaStatusEnum.Draft;
				}
				_context.TX_CorrectiveAction.Add(ca);
				_context.SaveChanges();
			}
		}

		public override async Task Update(TxCaDto dto)
		{
			try
			{
				var existing = _context.TX_CorrectiveAction.FirstOrDefault(o => o.CorrectiveActionID == dto.CorrectiveActionID);
				if (existing != null)
				{
					existing.Recomendation = dto.Recomendation;
					existing.ResponsibleDepartment = dto.ResponsibleDepartment;
					existing.DueDate = dto.DueDate;
					existing.CorrectiveActionPriority = dto.CorrectiveActionPriority;
					existing.ModifiedDate = DateHelper.WibNow;
					existing.Status = dto.Status;
					await _context.SaveChangesAsync();
				}
			}
			catch (System.Exception e)
			{

				throw;
			}

		}

		public async Task<List<TxCaDto>> GetList(string actionTrackingReference)
		{
			var q = await _context.TX_CorrectiveAction
				.Where(o => o.ActionTrackingID == actionTrackingReference && o.DataStatus != "deleted")
				.OrderBy(o => o.CreatedDate).ToListAsync();
			var result = q.Select(o => new TxCaDto(o)).ToList();

			// get user
			foreach (var ca in result)
			{
				FillEmpInfoToDto(ca);
			}
			return result;
		}

		public async Task SoftDeleteNotExists(string actId, List<string> caList)
		{
			if (!caList.Any())
			{
				return;
			}

			var caListWillDelete = _context
				.TX_CorrectiveAction.Where(o =>
				o.ActionTrackingID == actId &&
				caList.Contains(o.CorrectiveActionID));

			if (caListWillDelete.Any())
			{
				foreach (var ca in caListWillDelete)
				{
					ca.DataStatus = "deleted";
				}
				await _context.SaveChangesAsync();
			}
		}

		public async Task Sync(string actID, List<TxCaDto> caList, bool isSubmit)
		{
			var caStatus = CaStatusEnum.Draft;
			if (isSubmit)
			{
				caStatus = CaStatusEnum.Submitted;
			}


			var caListRef = caList.Where(o => o.CorrectiveActionID.ToUpper() != "DRAFT").Select(o => o.CorrectiveActionID).ToList();
			var caListWillCreate = caList.Where(o => o.CorrectiveActionID.ToUpper() == "DRAFT").ToList();
			var caListWillUpdate = caList.Where(o => o.CorrectiveActionID.ToUpper() != "DRAFT").ToList();

			var caListWillDelete = _context.TX_CorrectiveAction.Where(o =>
				o.ActionTrackingID == actID && !caListRef.Contains(o.CorrectiveActionID) && o.DataStatus != "deleted");

			// soft delete
			if (caListWillDelete.Any())
			{
				foreach (var ca in caListWillDelete)
				{
					ca.DataStatus = "deleted";
				}
				await _context.SaveChangesAsync();
			}


			// create if draft
			foreach (var ca in caListWillCreate)
			{
				ca.ActionTrackingID = actID;
				ca.Status = caStatus;
				await Create(ca);
			}


			// update
			foreach (var ca in caListWillUpdate)
			{
				ca.ActionTrackingID = actID;
				ca.Status = caStatus;
				await Update(ca);
			}

			// replace user list
			foreach (var ca in caList)
			{
				var deleteExisting = _context.TX_CorrectiveActionUser.Where(o => o.CorrectiveActionID == ca.CorrectiveActionID).ToList();
				if (deleteExisting.Any())
				{
					_context.TX_CorrectiveActionUser.RemoveRange(deleteExisting);
					_context.SaveChanges();
				}

				foreach (var caUser in ca.UserList)
				{
					caUser.CreatedBy = ca.CreatedBy;
					caUser.CreatedDate = ca.CreatedDate;
					caUser.CorrectiveActionID = ca.CorrectiveActionID;
					var userEntity = caUser.ToEntity();
					_context.TX_CorrectiveActionUser.Add(userEntity);
				}
			}
			_context.SaveChanges();
		}

		private string GenerateCaId()
		{
			var now = DateHelper.WibNow;
			var prefixId = $"CA-{now:yyyyMMdd}-";
			var lastId = _context.TX_CorrectiveAction
				.Where(o => o.CorrectiveActionID.StartsWith(prefixId))
				.OrderByDescending(o => o.CorrectiveActionID)
				.FirstOrDefault()?.CorrectiveActionID;

			var lastNumber = 1;
			if (!string.IsNullOrEmpty(lastId))
			{
				var lastNumberStr = lastId.Replace(prefixId, "");
				lastNumber = int.Parse(lastNumberStr);
				lastNumber++;
			}

			return $"{prefixId}{lastNumber:D4}";

		}

		public override async Task<TxCaDto> GetOne(string id)
		{
			var ca = await base.GetOne(id);
			FillEmpInfoToDto(ca);



			return ca;
		}

		private void FillEmpInfoToDto(TxCaDto ca)
		{
			var manager = _context.TX_CorrectiveActionUser.FirstOrDefaultWithNoLock(o => o.CorrectiveActionID == ca.CorrectiveActionID && o.Role == "ResponsibleManager");
			if (manager != null)
			{
				ca.ResponsibleManagerEmpName = manager.EmpName;
				ca.UserResponsibleManager = new TxCaUserDto(manager);
			}

			var pic1 = _context.TX_CorrectiveActionUser
			   .FirstOrDefaultWithNoLock(o => o.CorrectiveActionID == ca.CorrectiveActionID &&
			   o.Role == "PIC1");
			if (pic1 != null)
			{
				ca.Pic1 = pic1.PosId;//pic1.EmpId;
				ca.Pic1EmpName = pic1.EmpName;
				ca.UserPic1 = new TxCaUserDto(pic1);
			}

			var pic2 = _context.TX_CorrectiveActionUser.
			   FirstOrDefaultWithNoLock(o => o.CorrectiveActionID == ca.CorrectiveActionID &&
			   o.Role == "PIC2");
			if (pic2 != null)
			{
				ca.Pic2 = pic2.EmpId;
				ca.Pic2EmpName = pic2.EmpName;
				ca.UserPic2 = new TxCaUserDto(pic2);
			}
		}

		public async Task SaveFollowUp(TxCaDto dto)
		{
			var existing = _context.TX_CorrectiveAction.FirstOrDefault(o => o.CorrectiveActionID == dto.CorrectiveActionID);

			if (existing != null)
			{
				existing.FollowUpPlan = dto.FollowUpPlan;
				existing.CompletionDate = dto.CompletionDate;

				if (dto.IsSubmit)
				{
					existing.FlowCode = dto.FlowCode;
					existing.Status = "Open";
				}
				await _context.SaveChangesAsync();
			}
		}

		public async Task SaveProposedDueDate(TxCaDto dto)
		{
			var existing = _context.TX_CorrectiveAction.FirstOrDefault(o => o.CorrectiveActionID == dto.CorrectiveActionID);

			if (existing != null)
			{
				existing.ProposedDueDate = dto.ProposedDueDate;
				existing.ProposedDueDateData = dto.ProposedDueDateData;
				await _context.SaveChangesAsync();
			}
		}

		public async Task SaveOverdue(TxCaDto dto)
		{
			var existing = _context.TX_CorrectiveAction.FirstOrDefault(o => o.CorrectiveActionID == dto.CorrectiveActionID);

			if (existing != null)
			{
				existing.OverdueReason = dto.OverdueReason;
				existing.OverdueImpact = dto.OverdueImpact;
				existing.OverdueMitigation = dto.OverdueMitigation;
				existing.Status = "Request for Overdue";
				await _context.SaveChangesAsync();
			}
		}

		public async Task SaveWorkflowResponse(TxCaDto dto)
		{
			var currentStatus = dto.Status;
			var existing = _context.TX_CorrectiveAction.FirstOrDefault(o => o.CorrectiveActionID == dto.CorrectiveActionID);
			if (existing != null)
			{
				existing.Status = currentStatus;
				existing.TransID = dto.TransID;
				existing.FlowCode = dto.FlowCode;
				existing.ModifiedDate = DateHelper.WibNow;
				existing.ModifiedBy = dto.ModifiedBy;

				await _context.SaveChangesAsync();
			}

		}
	}
}
