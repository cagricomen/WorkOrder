using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrder.Business.Managers;
using WorkOrder.Core.Entities;
using WorkOrder.Web.Attributes;

namespace WorkOrder.Web.Controllers
{
    [Authorize]
    [ClaimRequirement("Admin")]
    public class AdminController : Controller
    {
        private readonly AdminManager adminManager;

        public AdminController(AdminManager adminManager)
        {
            this.adminManager = adminManager;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Index()
        {
            var list = adminManager.AllUser();

            return View(list);
        }

        public IActionResult NewUser()
        {
            var items = ListDepartmentItems();
            ViewBag.Departments = items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(User user)
        {
            var res = await adminManager.AddNewUser(user);
            if (res == null)
            {
                var items = ListDepartmentItems();
                ViewBag.Departments = items;
                return View();
            }
            else
            {
                var items = ListDepartmentItems();
                ViewBag.Departments = items;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await adminManager.EditUser(id);
            var items = ListDepartmentItems();
            ViewBag.Departments = items;
            return View("Edit", user);
        }

        public IActionResult Update(User user)
        {
            adminManager.UpdateUser(user);
            return RedirectToAction("Index");
        }

        
        public  IActionResult Delete(User user)
        {
            adminManager.DeleteUser(user);
            return RedirectToAction("Index");
        }

        
        public IActionResult Department()
        {
            var departments = adminManager.AllDepartments();
            return View("Department", departments);
        }

        
        public IActionResult NewDepartment()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> NewDepartment(Department department)
        {
            var res = await adminManager.AddNewDepartment(department);
            if(res == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Department");
            }
        }

        
        public async Task<IActionResult> EditDepartment(int id)
        {
            var department = await adminManager.EditDepartment(id);
            return View("EditDepartment", department);
        }

        
        public  IActionResult UpdateDepartment(Department department)
        {
            adminManager.UpdateDepartment(department);
            return RedirectToAction("Department");
        }

        
        public IActionResult DeleteDepartment(Department department)
        {
            adminManager.DeleteDepartment(department);
            return RedirectToAction("Department");
        }

        
        public IActionResult WorkPlace()
        {
            var workPlace = adminManager.AllWorkPlace();
            return View(workPlace);
        }

        
        public IActionResult NewWorkPlace()
        {
            var items = ListItemsWorkPlaceType();
            ViewBag.WorkPlaceTypes = items;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> NewWorkPlace(WorkPlace workPlace)
        {
            var res = await adminManager.NewWorkPlace(workPlace);
            if (res == null)
            {
                var items = ListItemsWorkPlaceType();
                ViewBag.WorkPlaceTypes = items;
                return View();
            }
            else
            {
                return RedirectToAction("WorkPlace");
            }
        }

        
        public async Task<IActionResult> EditWorkPlace(int id)
        {
            var department = await adminManager.EditWorkPlace(id);
            var items = ListItemsWorkPlaceType();
            ViewBag.WorkPLaceType = items;
            return View(department);
        }
        
        public IActionResult UpdateWorkPlace(WorkPlace workPlace)
        {
            adminManager.UpdateWorkPlace(workPlace);
            return RedirectToAction("WorkPlace");
        }

        
        public IActionResult DeleteWorkPlace(WorkPlace workPlace)
        {
            adminManager.DeleteWorkPlace(workPlace);
            return RedirectToAction("WorkPlace");
        }

        
        public IActionResult WorkPlaceType()
        {
            var workPlaceTypes = adminManager.AllWorkPlaceType();
            return View(workPlaceTypes);
        }

        
        public IActionResult NewWorkPlaceType()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> NewWorkPlaceType( WorkPLaceType workPLaceType)
        {
            var res = await adminManager.NewWorkPlaceType(workPLaceType);
            if (res == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("WorkPlaceType");
            }
        }

        
        public async Task<IActionResult> EditWorkPlaceType(int id)
        {
            var workPlaceType = await adminManager.EditWorkPlaceType(id);
            return View(workPlaceType);
        }

        
        public IActionResult UpdateWorkPlaceType(WorkPLaceType workPLaceType)
        {
            adminManager.UpdateWorkPlaceType(workPLaceType);
            return RedirectToAction("WorkPlaceType");
        }
        
        public IActionResult DeleteWorkPlaceType(WorkPLaceType workPLaceType)
        {
            adminManager.DeleteWorkPlaceType(workPLaceType);
            return RedirectToAction("WorkPlaceType");
        }

        
        public IActionResult Case()
        {
            var caseTypes = adminManager.AllCaseType();
            return View(caseTypes);
        }

        
        public IActionResult NewCase()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> NewCase(CaseType caseType)
        {
            var res = await adminManager.NewCase(caseType);
            if (res == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Case");
            }
        }
        public async Task<IActionResult> EditCaseType(int id)
        {
            var caseType = await adminManager.EditCaseType(id); ;
            return View(caseType);
        }

        
        public IActionResult UpdateCaseType(CaseType caseType)
        {
            adminManager.UpdateCaseType(caseType);
            return RedirectToAction("Case");
        }

        
        public IActionResult DeleteCase(CaseType caseType)
        {
            adminManager.DeleteCase(caseType);
            return RedirectToAction("Case");
        }

        public IActionResult Notification()
        {
            var notifications = adminManager.AllNotification();
            return View("Notification", notifications);
        }

        public IActionResult NewNotification()
        {
            var items = ListCaseItems();
            ViewBag.CaseTypes = items;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> NewNotification(Notification notification)
        {
            await adminManager.NewNotification(notification);
            return RedirectToAction("Notification");
        }
        public async Task<IActionResult> EditNotification(int id)
        {
            var department = await adminManager.EditNotification(id);
            var items = ListCaseItems();
            ViewBag.CaseTypes = items;
            return View(department);
        }

        public IActionResult UpdateNotification(Notification notification)
        {
            adminManager.UpdateNotification(notification);
            return RedirectToAction("Notification");
        }

        public IActionResult DeleteNotification(Notification notification)
        {
            adminManager.DeleteNotification(notification);
            return RedirectToAction("Notification");
        }
        private List<SelectListItem> ListDepartmentItems()
        {
            var department = adminManager.AllDepartments();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in department)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name.ToString() });
            }
            return items;
        }
        private List<SelectListItem> ListItemsWorkPlaceType()
        {
            var workPLaceTypes = adminManager.AllWorkPlaceType();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in workPLaceTypes)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name.ToString() });
            }
            return items;
        }
        private List<SelectListItem> ListCaseItems()
        {
            var caseTypes = adminManager.AllCaseType();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in caseTypes)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Name.ToString() });
            }
            return items;
        }


        [HttpGet]
        public async Task<string> GetUser()
        {
            var claimTypes = HttpContext.User.Claims.ToList();
            var userName = claimTypes[1].Value;
            var user = await adminManager.AdminFullName(userName);
            return user;
        }

        public async Task<IActionResult> Quit()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Home");
        }
    }
}
