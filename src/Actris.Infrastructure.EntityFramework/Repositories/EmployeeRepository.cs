using Actris.Abstraction.Model.Aiman;
using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class EmployeeRepository : BaseRepository, IEmployeeRepository
   {

      public EmployeeRepository(ActrisContext context, IConnectionProvider connection)
       : base(context, connection)
      {
      }

      public async Task<List<VwEmployeeLs>> GetEmployeeById(string empID)
      {
         var queryStr = $@"select DISTINCT EmpID, EmpName, DivisionDesc, DepartmentDesc, PositionTitle, EmpSubGroupName
                           FROM [PEKA].[Vw_AllMasterEmployee] WITH(NOLOCK)
                           WHERE EmpID = '{empID}'";

         using (var connection = OpenConnection())
         {
            var employeeList = await connection.QueryAsync<VwEmployeeLs>(queryStr);
            return employeeList.ToList();
         }
      }

      public async Task<List<VwEmployeeLs>> GetAllManager()
      {
         var queryStr = @"select distinct EmpID, EmpName, DivisionDesc, DepartmentDesc, PosID, PositionTitle, Abbrv, EmpSubGroupName
                            FROM [medical].[Vw_Employee] WITH(NOLOCK)
                            where 
                            isActive = 1 
                            and (DirectorateID in (10137522,10060575, 10120508, 10121421, 10122812 ,10123442,10123940) or CompanyCode = '5000') 
                            and EmployeeStatus = 'K' 
                            and (empsubgroupname in ('Executive','Jr. Executive','Senior Executive','Sr. Staff'))";

         using (var connection = OpenConnection())
         {
            var employeeList = await connection.QueryAsync<VwEmployeeLs>(queryStr);
            return employeeList.ToList();
         }
      }

      public async Task<Select2AjaxResponse> GetEmployeeByParent(string parentPosID, string keyword, int page)
      {
         var pageSize = 200;
         var fromTable = " FROM [PEKA].[Vw_AllMasterEmployee] WITH(NOLOCK) ";
         var queryStr = $@"select DISTINCT EmpID, EmpName, DivisionDesc, DepartmentDesc, PositionTitle, EmpSubGroupName
                           {fromTable}
                           WHERE EmpAccount IS NOT NULL AND ";

         if (string.IsNullOrEmpty(keyword))
         {
            keyword = "";
         }

         var whereStr = $" LOWER(EmpName) LIKE LOWER('%{keyword}%') ";
         if (!string.IsNullOrEmpty(parentPosID))
         {
            whereStr += $" AND ParentPosID = '{parentPosID}' ";
         }
         queryStr += $"{whereStr} " +
                   $"ORDER BY EmpName ASC " +
                   $"OFFSET {pageSize * (page - 1)} ROWS " +
                   $"FETCH NEXT {pageSize} ROWS ONLY";

         var countQuery = $"SELECT COUNT(DISTINCT EmpID) " +
                           $"{fromTable} " +
                           $"WHERE EmpAccount IS NOT NULL AND {whereStr} ";


         using (var connection = OpenConnection())
         {
            var employeeList = await connection.QueryAsync<VwEmployeeLs>(queryStr);
            var totalItem = (await connection.QueryAsync<int>(countQuery)).FirstOrDefault();

            return new Select2AjaxResponse
            {
               IsMoreItem = totalItem > pageSize * page,
               Items = employeeList.ToList(),
               TotalItem = totalItem
            };
         }




      }

      public async Task<TxCaUserDto> GetOneEmployee(string empID)
      {
         var queryStr = $@"select EmpID,
	                           EmpName,
	                           EmpBirth,
	                           EmpAccount,
	                           EmpEmail,
	                           ParentPosID,
	                           ParentPosTitle,
	                           EmpGroupID,
	                           EmpGroupName,
	                           EmpSubGroupID,
	                           EmpSubGroupName,
	                           KTP,
	                           Phone,
	                           AssignmentNumber,
	                           PosID,
	                           PositionTitle AS PosTitle,
	                           CompanyCode,
	                           CompanyName,
	                           HomeCompanyCode,
	                           CostCenter,
	                           CostCenterName,
	                           ControllingArea,
	                           Layer,
	                           PRL,
	                           KBO,
	                           NamaKBO,
	                           PersAreaID,
	                           PersAreaText,
	                           PersSubAreaID,
	                           PersSubAreaText,
	                           DirectorateID,
	                           DirectorateDesc,
	                           SectionID,
	                           SectionDesc,
	                           DivisionID,
	                           DivisionDesc,
	                           SubDivisionID,
	                           SubDivisionDesc,
	                           DepartmentID,
	                           DepartmentDesc,
	                           FunctionID,
	                           isChief,
	                           isSSO,
	                           Golper,
	                           empBirthPlace,
	                           Nationality,
	                           Gender,
	                           Religion,
	                           Address1,
	                           Address2,
	                           Address3,
	                           Phone2,
	                           MaritalStatus,
	                           BloodType,
	                           EmergencyContactName,
	                           EmergencyContactPhone,
	                           isActive,
	                           LogDataRefId,
	                           PrivateEmailAddress,
	                           EmployeeStatus,
	                           VendorID,
	                           VendorName,
	                           Abbrv,
	                           SyncDate,
	                           OrgUnitID
                           FROM [PEKA].[Vw_AllMasterEmployee] WITH(NOLOCK)
                           WHERE EmpID = '{empID}'";

         using (var connection = OpenConnection())
         {
            var employeeList = await connection.QueryAsync<TxCaUserDto>(queryStr);
            return employeeList.FirstOrDefault();
         }
      }

       public async Task<TxCaUserDto> GetOneEmployeeByPosId(string empID)
        {
            var queryStr = $@"select EmpID,
	                           EmpName,
	                           EmpBirth,
	                           EmpAccount,
	                           EmpEmail,
	                           ParentPosID,
	                           ParentPosTitle,
	                           EmpGroupID,
	                           EmpGroupName,
	                           EmpSubGroupID,
	                           EmpSubGroupName,
	                           KTP,
	                           Phone,
	                           AssignmentNumber,
	                           PosID,
	                           PositionTitle AS PosTitle,
	                           CompanyCode,
	                           CompanyName,
	                           HomeCompanyCode,
	                           CostCenter,
	                           CostCenterName,
	                           ControllingArea,
	                           Layer,
	                           PRL,
	                           KBO,
	                           NamaKBO,
	                           PersAreaID,
	                           PersAreaText,
	                           PersSubAreaID,
	                           PersSubAreaText,
	                           DirectorateID,
	                           DirectorateDesc,
	                           SectionID,
	                           SectionDesc,
	                           DivisionID,
	                           DivisionDesc,
	                           SubDivisionID,
	                           SubDivisionDesc,
	                           DepartmentID,
	                           DepartmentDesc,
	                           FunctionID,
	                           isChief,
	                           isSSO,
	                           Golper,
	                           empBirthPlace,
	                           Nationality,
	                           Gender,
	                           Religion,
	                           Address1,
	                           Address2,
	                           Address3,
	                           Phone2,
	                           MaritalStatus,
	                           BloodType,
	                           EmergencyContactName,
	                           EmergencyContactPhone,
	                           isActive,
	                           LogDataRefId,
	                           PrivateEmailAddress,
	                           EmployeeStatus,
	                           VendorID,
	                           VendorName,
	                           Abbrv,
	                           SyncDate,
	                           OrgUnitID
                           FROM [PEKA].[Vw_AllMasterEmployee] WITH(NOLOCK)
                           WHERE PosID = '{empID}'";

            using (var connection = OpenConnection())
            {
                var employeeList = await connection.QueryAsync<TxCaUserDto>(queryStr);
                return employeeList.FirstOrDefault();
            }
        }

        public async Task<TxCaUserDto> GetExecutiveEmployee(string PosID)
        {
            var queryStr = $@"select EmpID,
	                           EmpName,
	                           EmpBirth,
	                           EmpAccount,
	                           EmpEmail,
	                           ParentPosID,
	                           ParentPosTitle,
	                           EmpGroupID,
	                           EmpGroupName,
	                           EmpSubGroupID,
	                           EmpSubGroupName,
	                           KTP,
	                           Phone,
	                           AssignmentNumber,
	                           PosID,
	                           PositionTitle AS PosTitle,
	                           CompanyCode,
	                           CompanyName,
	                           HomeCompanyCode,
	                           CostCenter,
	                           CostCenterName,
	                           ControllingArea,
	                           Layer,
	                           PRL,
	                           KBO,
	                           NamaKBO,
	                           PersAreaID,
	                           PersAreaText,
	                           PersSubAreaID,
	                           PersSubAreaText,
	                           DirectorateID,
	                           DirectorateDesc,
	                           SectionID,
	                           SectionDesc,
	                           DivisionID,
	                           DivisionDesc,
	                           SubDivisionID,
	                           SubDivisionDesc,
	                           DepartmentID,
	                           DepartmentDesc,
	                           FunctionID,
	                           isChief,
	                           isSSO,
	                           Golper,
	                           empBirthPlace,
	                           Nationality,
	                           Gender,
	                           Religion,
	                           Address1,
	                           Address2,
	                           Address3,
	                           Phone2,
	                           MaritalStatus,
	                           BloodType,
	                           EmergencyContactName,
	                           EmergencyContactPhone,
	                           isActive,
	                           LogDataRefId,
	                           PrivateEmailAddress,
	                           EmployeeStatus,
	                           VendorID,
	                           VendorName,
	                           Abbrv,
	                           SyncDate,
	                           OrgUnitID
                           FROM [PEKA].[Vw_AllMasterEmployee] WITH(NOLOCK)
                           WHERE PosID = '{PosID}' AND EmpName not like '%Vacant Position%'";

            using (var connection = OpenConnection())
            {
                var employeeList = await connection.QueryAsync<TxCaUserDto>(queryStr);
                return employeeList.FirstOrDefault();
            }
        }
    }
}

