namespace Entities {
    public class Client {
        string name;
        string adress;
        uint cellphoneNumber;

        public Client(string name, string adress, uint cellphoneNumber) {
            this.name = name;
            this.adress = adress;
            this.cellphoneNumber = cellphoneNumber;
        }
    }
}