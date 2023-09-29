using System.Text.Json;

namespace tl2_tp4_2023_InakiPoch.Models;
public class OrderAccess {
    public List<Order> GetOrders() {
        List<Order> orders;
        var route = "DataFiles/orders.json";
        string document;
        using(var reader = new StreamReader(route)) {
            document = reader.ReadToEnd();
        }
        orders = JsonSerializer.Deserialize<List<Order>>(document);
        return orders;
    }

    public void SaveOrders(List<Order> orders) {
        var route = "DataFiles/orders.json";
        var data = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
        using(var writer = new StreamWriter(route)) {
            writer.WriteLine(data);
        }
    }
}
