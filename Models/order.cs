using System.Text.Json;
using System.Text.Json.Serialization;

namespace tl2_tp4_2023_InakiPoch.Models;
public enum Status { Pending, Completed }
public class Order {
    int orderNumber;
    string observation;
    Client client;
    Delivery delivery;
    Status orderStatus;

    public Order(int orderNumber, string observation, Client client, Status orderStatus) {
        this.orderNumber = orderNumber;
        this.observation = observation;
        this.client = new Client(client.Name, client.Adress, client.CellPhoneNumber);
        delivery = null;
        this.orderStatus = orderStatus;
    }

    public bool DeliveryAssigned() => delivery != null;
    public int OrderNumber { get => orderNumber; }
    public string Observation { get => observation; }
    public Client Client { get => client; }
    public Delivery Delivery { get => delivery; set { delivery = value; }}
    public Status OrderStatus { get => orderStatus; set { orderStatus = value; } }
}
