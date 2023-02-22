namespace AutoShop.Models
{
    public class CarCategory
    {
        public int Id { get; set; } 
        public string? TypeCar { get; set; }
        public string? CarModel { get; set;}
        public int Year{ get; set; }

        public string? Transmision { get; set; }

        public string? Wheel { get; set; }  

        public int Price { get; set; }
        public string? Description { get; set; }
        public string? CarPhotos { get; set; }

        public User? User { get; set; }
    }
}
