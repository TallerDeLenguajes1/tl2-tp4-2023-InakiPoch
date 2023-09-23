using System.Reflection.Metadata.Ecma335;

namespace tl2_tp4_2023_InakiPoch.Models;
public class Report {
    IEnumerable<Order> completedOrders;
    List<Delivery> deliveriesList;
    List<Order> pendingOrders;
    List<Order> totalOrders;
    int totalPayment;

    public Report(IEnumerable<Order> completedOrders, List<Delivery> deliveriesList, List<Order> pendingOrders, List<Order> totalOrders, int totalPayment) {
        this.completedOrders = completedOrders;
        this.deliveriesList = deliveriesList;
        this.pendingOrders = pendingOrders;
        this.totalOrders = totalOrders;
        this.totalPayment = totalPayment;
    }

    public int GetCompletedOrdersCount() {
        if(completedOrders != null)
            return completedOrders.Count();
        return 0;
    }

    public int GetPendingOrdersCount() => pendingOrders.Count;
    public int GetTotalOrdersCount() => totalOrders.Count;
    public int TotalPayment { get => totalPayment; }
}
