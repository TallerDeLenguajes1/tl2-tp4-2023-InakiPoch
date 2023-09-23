using System.Text.Json;
using tl2_tp4_2023_InakiPoch.Models;

namespace DataModels;
public static class DeliveryAccess {
    public static List<Delivery> GetDeliveries() {
        List<Delivery> deliveries;
        var route = "DataFiles/delivery-data.json";
        string document;
        using(var reader = new StreamReader(route)) {
            document = reader.ReadToEnd();
        }   
        deliveries = JsonSerializer.Deserialize<List<Delivery>>(document);
        return deliveries;
    }
}
