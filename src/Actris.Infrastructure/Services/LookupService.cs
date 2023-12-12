using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.DbView;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class LookupService : ILookupService
   {
      private readonly HttpContextBase _httpContext;
      private readonly ILookupRepository _repo;
      private readonly IEmployeeRepository _empRepo;
      private readonly IAimanApiService _aimanSvc;
      private static CacheService _cache = CacheService.GetInstance(nameof(LookupService));
      private CacheItemPolicy CacheExpiration => new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) };

      public LookupService(
          ILookupRepository repo,
          IAimanApiService aimanSvc,
          IEmployeeRepository empRepo)
      {
         _repo = repo;
         _aimanSvc = aimanSvc;
         _empRepo = empRepo;
      }
      public async Task<LookupList> GetActSourceList()
      {

         return await _cache.GetFromCacheAsync(
             nameof(GetActSourceList),
             "ALL", x => _repo.GetActSourceList(),
             CacheExpiration
         );
      }

      public async Task<LookupList> GetCaPriorityList()
      {

         return await _cache.GetFromCacheAsync(
             nameof(GetCaPriorityList),
             "ALL", x => _repo.GetCaPriorityList(),
             CacheExpiration
         );
      }

      public async Task<LookupList> GetDirectorateList()
      {
         var list = await _aimanSvc.GetDirectorate();

         // convert ke lookup model
         var result = new LookupList
         {
            Items = list.Select(item => new LookupItem
            {
               Text = item.DirectorateDesc,
               Value = item.DirectorateID
            }).ToList()
         };

         return result;
      }

      public async Task<LookupList> GetDivisionList()
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetDivision();

            list = list.Where(o => !string.IsNullOrEmpty(o.DivisionDesc)).ToList();

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.DivisionID, o.DivisionDesc)).ToLookupList();

            return result;
         }

         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetDivisionList), "ALL",
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetSubDivisionList()
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetSubDivision();

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.SubDivisionID, o.SubDivisionDesc)).ToLookupList();

            return result;
         }

         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetSubDivisionList), "ALL",
             x => getFromApi(), CacheExpiration);
      }


      private LookupList DummyOptionList(string prefix)
      {
         var ls = new LookupList
         {
            Items = new List<LookupItem>()
         };

         for (int i = 1; i <= 10; i++)
         {
            ls.Items.Add(new LookupItem
            {
               Text = $"{prefix} {i}",
               Value = $"{prefix}_{i}",
            });
         }
         return ls;
      }

      public async Task<LookupList> GetCompanyList()
      {

         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetCompany();

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.CompanyCode, o.CompanyName)).ToLookupList();

            return result;
         }
         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetCompanyList), "ALL",
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetWilayahKerjaList()
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetWorkingArea();

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.WKID, o.WKName)).ToLookupList();

            return result;
         }
         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetWilayahKerjaList), "ALL",
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetDepartmentList()
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetDepartment();

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.DepartmentID, o.DepartmentDesc)).ToLookupList();

            return result;
         }
         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetDepartmentList), "ALL",
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetLocationCompanyList(string companyCode)
      {
         // get from api with cache
         var allLocationFromApi = await _cache.GetFromCacheAsync("GetLocation", "ALL",
            x => _aimanSvc.GetLocationCompany(), CacheExpiration);

         // filter by company code
         var filteredLocation = allLocationFromApi.Where(o => o.CompanyCode == companyCode).ToList();

         // convert ke lookup model
         var result = filteredLocation.Select(item =>
            new LookupItem(
               item.LocationId,
               item.LocationName
            )).ToLookupList();

         result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.First()).ToList();

         return result;

      }

      public async Task<LookupList> GetDivisionListByDirectorateID(string directorateID)
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetPSAHierarchy($"[DirectorateID]='{directorateID}'");



            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.DivisionID, o.DivisionDesc)).ToLookupList();

            return result;
         }

         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetDivisionListByDirectorateID), directorateID,
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetCompanyListByDivisionID(string divisionID)
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetPSAHierarchy($"[DivisionID]='{divisionID}'");

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.CompanyCode, o.CompanyName)).ToLookupList();


            return result;
         }

         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetCompanyListByDivisionID), divisionID,
             x => getFromApi(), CacheExpiration);
      }

      public async Task<LookupList> GetWorkingAreaListByCompanyCode(string companyCode)
      {
         async Task<LookupList> getFromApi()
         {
            // get from api
            var list = await _aimanSvc.GetPSAHierarchy($"[CompanyCode]='{companyCode}'");

            // convert ke lookup model
            var result = list.Select(o => new LookupItem(o.EntityID, o.EntityName)).ToLookupList();

            return result;
         }

         // convert ke cache selama waktu yg ditentukan
         return await _cache.GetFromCacheAsync(nameof(GetWorkingAreaListByCompanyCode), companyCode,
             x => getFromApi(), CacheExpiration);
      }



      public async Task<Select2AjaxResponse> GetManagerList(string keyword, int page = 1)
      {
         if (keyword == null)
         {
            keyword = "";
         }
         keyword = keyword.ToLower();
         var pageSize = 200;

         // masukin cache biar search nya lebih cepet di memory cache
         var rawEmployeeList = await _cache.GetFromCacheAsync(nameof(GetManagerList), "ALL",
            x => _empRepo.GetAllManager(), CacheExpiration);

         // filter by keyword
         var filtered = rawEmployeeList.Where(o =>
             o.EmpName.ToLower().Contains(keyword) ||
             o.DepartmentDesc.ToLower().Contains(keyword) ||
             o.EmpID == keyword)
             .OrderBy(o => o.EmpName).ThenBy(o => o.DepartmentDesc);

         var totalItem = filtered.Count();
         var items = filtered.Take(pageSize).Skip(page - 1);

         // convert to select2AjaxResponse
         var result = new Select2AjaxResponse
         {
            TotalItem = totalItem,
            Items = items.ToList(),
            IsMoreItem = totalItem > pageSize * page
         };


         return result;
      }

      public async Task<Select2AjaxResponse> GetEmployeeListByParentEmpID(string parentEmpID, string keyword, int page = 1)
      {
         string parentPosID = null;
         // 1. get Parent PosID
         if (!string.IsNullOrEmpty(parentEmpID))
         {
            var lookup = await GetManagerList(parentEmpID);
            var parentEmp = lookup.Items.FirstOrDefault();

            if (parentEmp != null)
            {
               parentPosID = parentEmp.PosID;
            }
         }


         // 2. query employee dgn cache 10 min
         var keyCache = $"{parentEmpID}-{keyword}-{page}";
         var result = await _cache.GetFromCacheAsync(nameof(GetEmployeeListByParentEmpID),
            keyCache,
         x => _empRepo.GetEmployeeByParent(parentPosID, keyword, page),
         new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) });
         return result;


      }

      public async Task<List<VwEmployeeLs>> GetEmployeeList(string empID)
      {
         return await _cache.GetFromCacheAsync(nameof(GetEmployeeList), empID,
          x => _empRepo.GetEmployeeById(empID), CacheExpiration);
      }

      public async Task<LookupList> GetDirectorateList(string hierLvl2)
      {
         return await _cache.GetFromCacheAsync(nameof(GetDirectorateList), hierLvl2,
         x => _repo.GetDirectorateList(hierLvl2), CacheExpiration);
      }

      public async Task<LookupList> GetDivisiZonaList(string hierLvl2, string directorateID)
      {
         var cacheKey = $"{hierLvl2}-{directorateID}";
         return await _cache.GetFromCacheAsync(nameof(GetDivisiZonaList), cacheKey,
          x => _repo.GetDivisiZonaList(hierLvl2, directorateID), CacheExpiration);
      }

      public async Task<LookupList> GetCompanyList(string hierLvl2, string directorateID, string divisionID)
      {
         var cacheKey = $"{hierLvl2}-{directorateID}-{divisionID}";
         return await _cache.GetFromCacheAsync(nameof(GetCompanyList), cacheKey,
         x => _repo.GetCompanyList(hierLvl2, directorateID, divisionID), CacheExpiration);
      }

      public async Task<LookupList> GetWilayahKerjaList(string hierLvl2, string directorateID, string divisionID, string companyCode)
      {
         var cacheKey = $"{hierLvl2}-{directorateID}-{divisionID}-{companyCode}";
         return await _cache.GetFromCacheAsync(nameof(GetWilayahKerjaList), cacheKey,
         x => _repo.GetWilayahKerjaList(hierLvl2, directorateID, divisionID, companyCode), CacheExpiration);
      }

      public async Task<LookupList> GetCaOverdueImpactList()
      {
         return await _cache.GetFromCacheAsync(
                nameof(GetCaOverdueImpactList),
                "ALL", x => _repo.GetCaOverdueImpactList(),
                CacheExpiration
            );
      }
   }
}
