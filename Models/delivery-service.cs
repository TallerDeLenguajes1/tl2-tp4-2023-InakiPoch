using System.Text.Json;

namespace tl2_tp4_2023_InakiPoch.Models;
public class DeliveryService {
    const int ORDER_PRICE = 500;
    static DeliveryService m_DeliveryService;
    DeliveryAccess deliveriesAccess;
    OrderAccess ordersAccess;
    string name;
    string cellphoneNumber;
    List<Delivery> deliveriesList;
    List<Order> totalOrders;

    public DeliveryService(string name, string cellphoneNumber, List<Delivery> deliveriesList) {
        this.name = name;
        this.cellphoneNumber = cellphoneNumber;
        this.deliveriesList = deliveriesList;
        totalOrders = new List<Order>();
    }

    public void AddOrder(Order order) {
        totalOrders.Add(order);
        string route = "DataFiles/orders.json";
        var jsonFile = JsonSerializer.Serialize(totalOrders, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(route, jsonFile);
        ordersAccess.SaveOrders(totalOrders);
    }

    public void AddDelivery(Delivery delivery) {
        deliveriesList.Add(delivery);
        deliveriesAccess.SaveDeliveries(deliveriesList);
    }

    public bool AssignOrder(int orderId, int deliveryId) {
        if(!deliveriesRemain())
            return false;
        Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
        Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
        if(!canHaveOrders(targetDelivery))
            return false;
        targetOrder.Delivery = targetDelivery;
        ordersAccess.SaveOrders(totalOrders);
        return true;
    }

    public bool ChangeStatus(int orderId, int newState) {
        Status status;
        Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
        if(targetOrder.OrderStatus == Status.Completed)
            return false;
        var result = Enum.GetName(typeof(Status), newState);
        if(Status.TryParse(result, out status)) {
            targetOrder.OrderStatus = status;
            ordersAccess.SaveOrders(totalOrders);
            return true;
        }
        return false;
    }

    public bool ReasignOrder(int orderId, int deliveryId) {
        Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
        Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
        if(!targetDelivery.IsFull()) {
            targetOrder.Delivery = targetDelivery;
            ordersAccess.SaveOrders(totalOrders);
            return true;
        }
        return false;
    }

    public int DeliveryPayment(int deliveryID) {
        var ordersList = from order in totalOrders where order.Delivery.Id == deliveryID select order;
        var ordersCompleted = from order in ordersList where order.OrderStatus == Status.Completed select order;
        return ORDER_PRICE * ordersCompleted.Count();
    }

    public Report GenerateReport() {
        var completedOrders = from order in totalOrders where order.OrderStatus == Status.Completed select order;
        int totalPayment = 0;
        foreach(Delivery delivery in deliveriesList) {
            totalPayment += DeliveryPayment(delivery.Id);
        }
        return new Report(completedOrders, totalOrders, totalPayment);
    }

    public static DeliveryService Instantiate() {
        if(m_DeliveryService == null) {
            var serviceAccess = new DeliveryServiceAccess();
            m_DeliveryService = serviceAccess.GetDeliveryService();
            m_DeliveryService.ordersAccess = new OrderAccess();
            m_DeliveryService.deliveriesAccess = new DeliveryAccess();
            m_DeliveryService.deliveriesList = m_DeliveryService.deliveriesAccess.GetDeliveries();
            m_DeliveryService.totalOrders = m_DeliveryService.ordersAccess.GetOrders();
        }
        return m_DeliveryService;
    }

    private bool deliveriesRemain() {
        foreach(Delivery delivery in deliveriesList) {
            if(delivery.IsFull()) return false;
        }
        return true;
    }

    private bool canHaveOrders(Delivery delivery) => !delivery.IsFull();
    public List<Order> GetOrders() => ordersAccess.GetOrders(); 
    public List<Delivery> GetDeliveries() => deliveriesAccess.GetDeliveries();
    public Order GetOrder(int orderId) => totalOrders.Single(order => order.OrderNumber == orderId);
    public Delivery GetDelivery(int deliveryId) => deliveriesList.Single(delivery => delivery.Id == deliveryId);

    public string Name { get => name; }
    public string CellphoneNumber { get => cellphoneNumber; }
    public List<Delivery> DeliveriesList { get => deliveriesList; }
    public List<Order> TotalOrders { get => totalOrders; }

}
