using System.Text.Json;

namespace tl2_tp4_2023_InakiPoch.Models;
public class DeliveryServiceAccess {
    public DeliveryService GetDeliveryService() {
        List<DeliveryService> service;
        var route = "DataFiles/delivery-service-data.json";
        string document;
        using(var reader = new StreamReader(route)) {
            document = reader.ReadToEnd();
        }
        service = JsonSerializer.Deserialize<List<DeliveryService>>(document);
        return service[0];
    }
}