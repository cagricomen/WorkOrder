using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;
using WorkOrder.Core.Repositories;

namespace WorkOrder.Data.Repositories
{
    public class WorkOrderRepository : GenericRepository<WorkOrders>, IWorkOrdersRepository
    {
        public WorkOrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<WorkOrders>> GetWorkOrdersWithCategory()
        {
            return await _appDbContext.WorkOrders.Include(x => x.User).Include(x => x.Department).Include(x => x.CaseType).Include(x => x.WorkPlace).ToListAsync();
        }
    }
}
