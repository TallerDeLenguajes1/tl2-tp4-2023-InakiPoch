namespace tl2_tp4_2023_InakiPoch.Models;
public class Client {
    string name;
    string adress;
    uint cellphoneNumber;

    public Client(string name, string adress, uint cellphoneNumber) {
        this.name = name;
        this.adress = adress;
        this.cellphoneNumber = cellphoneNumber;
    }

    public string Name { get => name; }
    public string Adress { get => adress; }
    public uint CellPhoneNumber { get => cellphoneNumber; }
}
