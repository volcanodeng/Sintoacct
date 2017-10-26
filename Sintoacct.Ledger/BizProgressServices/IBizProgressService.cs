using System.Collections.Generic;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizProgressService : IDependency
    {
        WorkOrder GetWorkOrder(long bizId);
        WorkOrder GetMyWorkOrder(long bizId);
        List<WorkOrder> GetMyWorkOrders();
        List<WorkOrder> GetMyWorkOrders(int pageIndex, int pageSize);
        WorkOrder SaveWorkOrder(WorkOrderViewModel bizProg);

        void DeleteWorkOrder(WorkOrderDelViewModel workOrder);
    }
}