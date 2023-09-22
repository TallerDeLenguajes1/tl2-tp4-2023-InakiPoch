using ReportManager;
using Data;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Entities {
    public class DeliveryService {
        const int ORDER_PRICE = 500;
        static DeliveryService m_DeliveryService;
        string name;
        string cellphoneNumber;
        List<Delivery> deliveriesList;
        List<Order> pendingOrders;
        List<Order> totalOrders;

        public DeliveryService(string name, string cellphoneNumber, List<Delivery> deliveriesList) {
            this.name = name;
            this.cellphoneNumber = cellphoneNumber;
            this.deliveriesList = deliveriesList;
            pendingOrders = new List<Order>();
            totalOrders = new List<Order>();
        }

        public void AddOrder(Order order) {
            pendingOrders.Add(order);
            totalOrders.Add(order);
            string route = "../order.json";
            var jsonFile = JsonSerializer.Serialize(totalOrders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(route, jsonFile);
        }
        
        public bool AssignOrder(int orderId, int deliveryId) {
            if(!deliveriesRemain())
                return false;
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
            Order targetOrder = pendingOrders.Single(order => order.OrderNumber == orderId);
            if(!canHaveOrders(targetDelivery))
                return false;
            targetOrder.Delivery = targetDelivery;
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
                return true;
            }
            return false;
        }

        public bool ReasignOrder(int orderId, int deliveryId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
            if(!targetDelivery.IsFull()) {
                targetOrder.Delivery = targetDelivery;
                return true;
            }
            return false;
        }

        public int DeliveryPayment(int deliveryID) {
            var ordersList = from order in totalOrders where order.Delivery?.Id == deliveryID select order;
            var ordersCompleted = from order in ordersList where order.OrderStatus == Status.Completed select order;
            return ORDER_PRICE * ordersCompleted.Count();
        }

        public Report GenerateReport() {
            var completedOrders = from order in totalOrders where order.OrderStatus == Status.Completed select order;
            int totalPayment = 0;
            foreach(Delivery delivery in deliveriesList) {
                totalPayment += DeliveryPayment(delivery.Id);
            }
            return new Report(completedOrders, deliveriesList, pendingOrders, totalOrders, totalPayment);
        }

        public static DeliveryService Instantiate() {
            if(m_DeliveryService == null) {
                DataAdress data = new JSONData();
                m_DeliveryService = data.GetService().First();
            }
            return m_DeliveryService;
        }

        private bool deliveriesRemain() {
            foreach(Delivery delivery in deliveriesList) {
                if(delivery.IsFull()) return false;
            }
            return true;
        }

        private bool canHaveOrders(Delivery delivery) => !delivery.IsFull() && pendingOrders.Any();


        public List<Order> PendingOrders { get => pendingOrders; }
        public List<Order> TotalOrders { get => totalOrders; }
        public List<Delivery> DeliveriesList { get => deliveriesList; }
        public string Name { get => name; }
        public string CellphoneNumber { get => cellphoneNumber; }
    }
}