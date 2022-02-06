using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;

namespace WorkOrder.Core.Repositories
{
    public interface IWorkOrdersRepository : IGenericRepository<WorkOrders>
    {
        Task<List<WorkOrders>> GetWorkOrdersWithCategory();
    }
}
