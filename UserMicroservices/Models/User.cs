namespace UserMicroservice.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; } 
        public string Role { get; set; }
        public ICollection<Guid> OrderIds { get; set; }  
        public ICollection<Guid> AddressIds { get; set; }
        public Guid? PaymentId { get; set; }  
        public Guid? CartId { get; set; }
    }
}
