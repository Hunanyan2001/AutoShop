namespace AutoShop.Models
{
    public class UserInformation
    {
        public string? Id { get; set; }
        public string? FirstName { get;set; }
        public string? LastName { get;set; }

        public string? UserName { get; set; }

        public string? Email { get; set; } 
        public string? Address { get; set; }

        public string? Address2 { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? PaymentMethod { get; set; }

        public string? NameOnCard { get; set; }

        public string? CreditCardNumber { get; set; }

        public string? Experiation { get; set; }

        public string? CVV { get; set; }
        public string? PhoneNumber { get; set; }

        public User? User { get; set; }
    }
}
