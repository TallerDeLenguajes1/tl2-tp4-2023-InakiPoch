using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using tl2_tp4_2023_InakiPoch.Models;

namespace DataModels;
public abstract class DataAdress {
    public abstract List<Delivery> GetDeliveries();
    public abstract List<DeliveryService> GetService();
    public void CreateJSONFile(string route) {
        var csvFile = new List<string[]>();
        var lines = File.ReadAllLines(route);
        foreach(string line in lines.Skip(1)) {
            csvFile.Add(line.Split(','));
        }
        var properties = lines[0].Split(',');
        var listObjResult = new List<Dictionary<string, string>>();
        for(int i = 0; i < lines.Length - 1; i++) {
            var objResult = new Dictionary<string, string>();
            for(int j = 0; j < properties.Length; j++) {
                objResult.Add(properties[j], csvFile[i][j]);
            }
            listObjResult.Add(objResult);
        }
        var jsonFile = System.Text.Json.JsonSerializer.Serialize(listObjResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText($"{route.Split('.')[0]}.json", jsonFile);
    }
}

// public class CSVData : DataAdress {
//     public override List<Delivery> GetDeliveries() {
//         List<Delivery> workers = new List<Delivery>();
//         using(var reader = new StreamReader("../delivery-data.csv")) {
//             while(!reader.EndOfStream) {
//                 string line = reader.ReadLine();
//                 if(line != null) {
//                     var splits = line.Split(',');
//                     workers.Add(new Delivery(splits[0], splits[1], splits[2], splits[3]));
//                 }
//             }
//         }
//         return workers;
//     }

//     public override List<DeliveryService> GetService() {
//         List<DeliveryService> service = new List<DeliveryService>();
//         List<Delivery> deliveries = GetDeliveries();
//         using(var reader = new StreamReader("../delivery-service-data.csv")) {
//             reader.ReadLine();
//             while(!reader.EndOfStream) {
//                 string line = reader.ReadLine();
//                 if(line != null) {
//                     var splits = line.Split(',');
//                     if(deliveries != null) {
//                         service.Add(new DeliveryService(splits[0], splits[1], deliveries));
//                         return service;
//                     }
//                 }
//             }
//         }
//         return null;
//     }
// }

public class JSONData : DataAdress {
    public override List<Delivery> GetDeliveries() {
        List<Delivery> deliveriesList;
        using(var reader = new StreamReader("DataFiles/delivery-data.json")) {
            deliveriesList = System.Text.Json.JsonSerializer.Deserialize<List<Delivery>>(reader.ReadToEnd());
        }
        return deliveriesList;
    }

    public override List<DeliveryService> GetService() {
        string dataPath = "DataFiles/delivery-service-data.json";
        List<DeliveryService> service;
        List<Delivery> deliveriesList = GetDeliveries();
        JArray jsonArray = JArray.Parse(File.ReadAllText(dataPath));
        foreach(JObject item in jsonArray) {
            item["DeliveriesList"] = JArray.Parse(JsonConvert.SerializeObject(deliveriesList));
        }
        File.WriteAllText(dataPath, jsonArray.ToString());
        using(var reader = new StreamReader(dataPath)) {
            service = JsonConvert.DeserializeObject<List<DeliveryService>>(reader.ReadToEnd());
        }
        return service;
    }
}
