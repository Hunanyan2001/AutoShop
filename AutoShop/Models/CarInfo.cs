namespace AutoShop.Models
{
    public class CarInfo
    {
        public int Id { get; set; }
        public string? TypeCar { get; set; }
        public string? CarModel { get; set; }
        public int StartingYear { get; set; }

        public int EndingYear { get; set;}
        public string? Transmision { get; set; }

        public string? Wheel { get; set; }

        public int StartingPrice { get; set; }

        public int EndingPrice { get; set;}

    }
}
