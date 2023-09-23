using System.Dynamic;

namespace tl2_tp4_2023_InakiPoch.Models;
public class Delivery {
    static int MAX_ORDERS = 5;
    int ordersNumber;
    int id;
    string name;
    string address;
    string cellphoneNumber;

    public Delivery(int id, string name, string address, string cellphoneNumber) {
        this.id = id;
        this.name = name;
        this.address = address;
        this.cellphoneNumber = cellphoneNumber;
        ordersNumber = 0;
    }

    public bool IsFull() => ordersNumber == MAX_ORDERS;
    public void IncreaseCurrentOrders() => ordersNumber++;
    public int Id { get => id; }
    public string Name { get => name; }
    public string Address { get => address; }
    public string CellphoneNumber { get => cellphoneNumber; }
}
