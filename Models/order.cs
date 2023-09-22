namespace Entities {
    public enum Status { Pending, Completed }
    public class Order {
        int orderNumber;
        string observation;
        Client client;
        Delivery delivery;
        Status orderStatus;

        public Order(int orderNumber, string observation, string clientName, string clientAdress, uint clientNumber) {
            this.orderNumber = orderNumber;
            this.observation = observation;
            client = new Client(clientName, clientAdress, clientNumber);
            delivery = null;
            orderStatus = Status.Pending;
        }

        public bool DeliveryAssigned() => delivery != null;
        public Status OrderStatus { get => orderStatus; set { orderStatus = value; } }
        public int OrderNumber { get => orderNumber; }
        public Delivery Delivery { get => delivery; set { delivery = value; }}
    }
}