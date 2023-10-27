using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Presentation.Client.Models
{
    public class Customer
    {
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string BankAccountNumber { get; set; }
    }
}
