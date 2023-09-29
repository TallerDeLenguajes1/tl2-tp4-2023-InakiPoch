using System.Text.Json;

namespace tl2_tp4_2023_InakiPoch.Models;
public class DeliveryAccess {
    public List<Delivery> GetDeliveries() {
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
