using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;
using WorkOrder.Core.Repositories;

namespace WorkOrder.Business.Managers
{
    public class UserManager
    {
        private readonly IWorkOrdersRepository _workOrderRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<CaseType> _caseTypeRepository;

        public UserManager(IWorkOrdersRepository workOrderRepository, IGenericRepository<User> userRepository, IGenericRepository<CaseType> caseTypeRepository)
        {
            _workOrderRepository = workOrderRepository;
            _userRepository = userRepository;
            _caseTypeRepository = caseTypeRepository;
        }

        public async Task<List<WorkOrders>> UserWorkOrder(string userName)
        {
            List<WorkOrders> workOrders1 = new List<WorkOrders>();
            var userId = await _userRepository.SingleOrDefaultAsync(x => x.UserName == userName);
            var workOrders = await _workOrderRepository.GetWorkOrdersWithCategory();
            foreach (var item in workOrders)
            {
                if((item.CaseType.Name == "İş Emri Verildi" || item.CaseType.Name == "İş Yapılıyor") && item.UserId == userId.Id)
                {
                    workOrders1.Add(item);
                }
            }
            return workOrders1;
        }

        public async Task<WorkOrders> StartWorkOrder(int id)
        {
            var workOrder = await _workOrderRepository.GetByIdAsync(id);
            var caseType = await _caseTypeRepository.SingleOrDefaultAsync(x => x.Name == "İş Yapılıyor");

            if(workOrder.CaseTypeId != caseType.Id)
            {
                workOrder.CaseType = caseType;
                workOrder.CaseTypeId = caseType.Id;
                workOrder.DateOfStart = DateTime.Now;
            }
            _workOrderRepository.Update(workOrder);
            return workOrder;
        }
        public async Task<string> UserFullName(string userName)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName == userName);

            return $"{user.Name} {user.LastName}";
        }
    }
}
