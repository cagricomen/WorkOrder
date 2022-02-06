using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrder.Business.Managers;
using WorkOrder.Core.DTOs;
using WorkOrder.Core.Entities;
using WorkOrder.Web.Attributes;

namespace WorkOrder.Web.Controllers
{
    [Authorize]
    [ClaimRequirement("Manager")]
    public class ManagerController : Controller
    {
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var workOrders = await _managerService.AllWorkOrders();
            return View(workOrders);
        }

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult NewWorkOrder()
        {
            var departmentItems = ListDepartmentItems();
            var userItems = ListUserItems(departmentItems[0].Text);
            var caseTypesItems = ListCaseItems();
            var workPlaceItems = ListWorkPlaceItems();
            ViewBag.Departments = departmentItems;
            ViewBag.Users = userItems;
            ViewBag.CaseTypes = caseTypesItems;
            ViewBag.WorkPlaces = workPlaceItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewWorkOrder(WorkOrdersDto workOrders)
        {
            var res = await _managerService.NewWorkOrder(workOrders);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditWorkOrder(int id)
        {
            var workOrder = await _managerService.EditWorkOrder(id);
            var departmentItems = ListDepartmentItems();
            var userItems = ListUserItems("Boya");
            var caseTypesItems = ListCaseItems();
            var workPlaceItems = ListWorkPlaceItems();
            ViewBag.Departments = departmentItems;
            ViewBag.Users = userItems;
            ViewBag.CaseTypes = caseTypesItems;
            ViewBag.WorkPlaces = workPlaceItems;
            return View(workOrder);
        }

        public async Task<IActionResult> UpdateWorkOrder(WorkOrdersDto workOrders)
        {
            await _managerService.UpdateWorkOrder(workOrders);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWorkOrder(WorkOrders workOrders)
        {
            _managerService.DeleteWorkOrder(workOrders);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> WorkAssignment()
        {
            var works = await _managerService.WorkAssignment();
            return View(works);
        }

      
        
        public async Task<IActionResult> UpdateWorkAssignment(int id)
        {
            await _managerService.UpdateWorkAssignment(id);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> WorkOrdersWaiting()
        {
            var workOrders = await _managerService.WorkOrdersWaiting();
            return View(workOrders);
        }
        
        public async Task<IActionResult> UpdateWorkOrders(int id)
        {
            var work = await _managerService.UpdateWorkOrders(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public List<User> ChangeDepValue(string dep)
        {

            var res = _managerService.ChangeDepValue(dep);
            return res;
        }

        public async Task<IActionResult> ReportData()
        {
            var items = await _managerService.AllReportData();
            return View(items);
        }

        [HttpGet]
        public async Task<string> GetUser()
        {
            var claimTypes = HttpContext.User.Claims.ToList();
            var userName = claimTypes[1].Value;
            var user = await _managerService.ManagerFullName(userName);
            return user;
        }

        public async Task<IActionResult> Quit()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("HomePage");
        }
        private List<SelectListItem> ListDepartmentItems()
        {
            var department = _managerService.AllDepartment();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in department)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name});
            }
            return items;
        }
        private List<SelectListItem> ListUserItems(string departmentName)
        {
            var users = _managerService.UserItems(departmentName);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in users)
            {
                items.Add(new SelectListItem { Text = item.Name +" "+  item.LastName, Value = item.UserName+ item.LastName });
            }
            return items;
        }

        private List<SelectListItem> ListCaseItems()
        {
            var caseTypes = _managerService.CaseTypeItems();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in caseTypes)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
            return items;
        }

        private List<SelectListItem> ListWorkPlaceItems()
        {
            var workPlaces = _managerService.WorkPlaceItems();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in workPlaces)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name,  });
            }
            return items;
        }
    }
}
