using System.Text.Json;
using tl2_tp4_2023_InakiPoch.Models;

namespace DataModels;
public static class OrdersAccess {
    public static List<Order> GetOrders() {
        List<Order> orders;
        var route = "DataFiles/orders.json";
        string document;
        using(var reader = new StreamReader(route)) {
            document = reader.ReadToEnd();
        }
        orders = JsonSerializer.Deserialize<List<Order>>(document);
        return orders;
    }

    public static void SaveOrders(List<Order> orders) {
        var route = "DataFiles/orders.json";
        var data = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
        using(var writer = new StreamWriter(route)) {
            writer.WriteLine(data);
        }
    }
}
