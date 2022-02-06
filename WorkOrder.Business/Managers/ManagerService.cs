using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrder.Core.DTOs;
using WorkOrder.Core.Entities;
using WorkOrder.Core.Repositories;

namespace WorkOrder.Business.Managers
{
    public class ManagerService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IWorkOrdersRepository _workOrderRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<WorkPlace> _workPlaceRepository;
        private readonly IGenericRepository<WorkPLaceType> _workPlaceTypeRepository;
        private readonly IGenericRepository<CaseType> _caseTypeRepository;

        public ManagerService(IWorkOrdersRepository workOrderRepository, IGenericRepository<Department> departmentRepository, IGenericRepository<WorkPlace> workPlaceRepository, IGenericRepository<WorkPLaceType> workPlaceTypeRepository, IGenericRepository<CaseType> caseTypeTypeRepository, IGenericRepository<User> userRepository)
        {
            _workOrderRepository = workOrderRepository;
            _departmentRepository = departmentRepository;
            _workPlaceRepository = workPlaceRepository;
            _workPlaceTypeRepository = workPlaceTypeRepository;
            _caseTypeRepository = caseTypeTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<List<WorkOrders>> AllWorkOrders()
        {
            var workOrders = await _workOrderRepository.GetWorkOrdersWithCategory();
            return workOrders;
        }

        public async Task<WorkOrders> NewWorkOrder(WorkOrdersDto workOrders)
        {
            var firstName = workOrders.User.Split(" ").First();
            var lastName = workOrders.User.Split(" ").Last();
            var caseType = await _caseTypeRepository.SingleOrDefaultAsync(x => x.Name == workOrders.CaseType);
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Name == firstName && x.LastName == lastName);
            var dep = await _departmentRepository.SingleOrDefaultAsync(x => x.Name == workOrders.Department);
            var work = await _workPlaceRepository.SingleOrDefaultAsync(x => x.Name == workOrders.WorkPlace);
            var createWorkOrder = new WorkOrders
            {
                DateOfFinish = workOrders.DateOfFinish,
                DateOfOrder = DateTime.Now,
                DateOfStart = DateTime.Now,
                DateOfWaiting = DateTime.Now,
                Department = dep,
                Job = workOrders.Job,
                WorkPlace = work,
                CaseTypeId = caseType.Id,
                ManagerName = workOrders.ManagerName,
                User = user,
                Result = workOrders.Result,
                CaseType = caseType,
                DepartmentId = dep.Id,
                UserId = user.Id,
                WorkPlaceId = work.Id

            };
            await _workOrderRepository.AddAsync(createWorkOrder);
            return createWorkOrder;
        }

        public async Task<WorkOrders> EditWorkOrder(int id)
        {
            var res = await _workOrderRepository.GetByIdAsync(id);
            return res;

        }
        public async Task<WorkOrders> UpdateWorkOrder(WorkOrdersDto workOrders)
        {
            var orders = await _workOrderRepository.SingleOrDefaultAsync(x=> x.Id == workOrders.Id);
            var firstName = workOrders.User.Split(" ").First();
            var lastName = workOrders.User.Split(" ").Last();
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Name == firstName && x.LastName == lastName);
            var dep = await _departmentRepository.SingleOrDefaultAsync(x => x.Name == workOrders.Department);
            var workPlace = await _workPlaceRepository.SingleOrDefaultAsync(x => x.Name == workOrders.WorkPlace);
            var caseType = await _caseTypeRepository.SingleOrDefaultAsync(x => x.Name == workOrders.CaseType);
            orders.WorkPlace = workPlace;
            orders.WorkPlaceId = workPlace.Id;
            orders.Department = dep;
            orders.DepartmentId = dep.Id;
            orders.CaseType = caseType;
            orders.CaseTypeId = caseType.Id;
            orders.Job = workOrders.Job;
            orders.Result = workOrders.Result;
            orders.User = user;
            orders.UserId = user.Id;
            _workOrderRepository.Update(orders);
            
            return orders;
        }
        public void DeleteWorkOrder(WorkOrders workOrders)
        {
            _workOrderRepository.Delete(workOrders);
        }

        public async Task<List<WorkOrders>> WorkAssignment()
        {
            var workOrders = await _workOrderRepository.GetWorkOrdersWithCategory();
            var result = workOrders.Where(x => x.CaseType.Name == "Beklemede" || x.CaseType.Name == "Talep Edildi").ToList();
            return result;
        }

        public async Task<WorkOrders> UpdateWorkAssignment(int id)
        {
            var work = await _workOrderRepository.GetWorkOrdersWithCategory();
            var caseType = await _caseTypeRepository.SingleOrDefaultAsync(x => x.Name == "İş Emri Verildi");
            var res = work.Where(x => x.Id == id).FirstOrDefault();
            res.CaseType = caseType;
            res.DateOfOrder = DateTime.Now;
            _workOrderRepository.Update(res);
            return res;
        }
        public async Task<List<WorkOrders>> WorkOrdersWaiting()
        {
            var workOrders = await _workOrderRepository.GetWorkOrdersWithCategory();
            var res = workOrders.Where(x => x.CaseType.Name != "Beklemede" || x.CaseType.Name !="İptal Edildi" || x.CaseType.Name != "Tamamlandı").ToList();
            return res;
        }

        public async Task<WorkOrders> UpdateWorkOrders(int id)
        {
            var caseType = await _caseTypeRepository.SingleOrDefaultAsync(x => x.Name == "Beklemede");
            var work = await _workOrderRepository.SingleOrDefaultAsync(x => x.Id == id);
            work.CaseType = caseType;
            work.DateOfWaiting = DateTime.Now;
            _workOrderRepository.Update(work);
            return work;
        }
        public List<User> ChangeDepValue(string dep)
        {

            var res = _userRepository.Where(x => x.Department == dep).ToList();
            return res;
        }

        public List<Department> AllDepartment()
        {
            var departments = _departmentRepository.GetAll().ToList();
            return departments;
        }

        public List<User> UserItems(string departmentName)
        {
            var users = _userRepository.Where(x => x.Department == departmentName && x.UserRole.Equals("User")).ToList();
            return users;
        }
        public List<CaseType> CaseTypeItems()
        {
            var caseTypeItems = _caseTypeRepository.GetAll().ToList();
            return caseTypeItems;
        }
        public List<WorkPlace> WorkPlaceItems()
        {
            var workPlaces = _workPlaceRepository.GetAll().ToList();
            return workPlaces;
        }

        public async Task<ReportDataDto> AllReportData()
        {
            var workOrders = await _workOrderRepository.GetWorkOrdersWithCategory();
            var rdto = new ReportDataDto
            {
                AllWork = workOrders.Count(),
                WaitingWork = workOrders.Where(x => x.CaseType.Name == "Beklemede").Count(),
                CanselWork = workOrders.Where(x => x.CaseType.Name == "İptal Edildi").Count(),
                DoingWork = workOrders.Where(x => x.CaseType.Name == "İş Yapılıyor").Count(),
                DoneWork = workOrders.Where(x => x.CaseType.Name == "Tamamlandı").Count(),
                RequestWork = workOrders.Where(x => x.CaseType.Name == "Talep Edildi").Count(),
                OrderWork = workOrders.Where(x => x.CaseType.Name == "İş Emri Verildi").Count()
            };
            return rdto;
            
        }
        public async Task<string> ManagerFullName(string userName)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName == userName);

            return $"{user.Name} {user.LastName}";
        }
    }
}