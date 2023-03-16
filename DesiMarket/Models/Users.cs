using System.ComponentModel.DataAnnotations;

namespace DesiMarket.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int Pincode { get; set; }
       
    }
}
