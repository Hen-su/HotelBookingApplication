using System.ComponentModel.DataAnnotations;

namespace HotelBookingApplication.Models
{
    public class GuestDetails
    {
        public int GuestDetailsId { get; set; }
        public int ReservationId { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email")]
        [StringLength(50)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
        ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        [Display(Name = "Address Line 1")]
        [StringLength(100)]
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        [Required(ErrorMessage = "Please enter your City")]
        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your state")]
        [Display(Name = "State")]
        [StringLength(10)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter your Post Code")]
        [Display(Name = "Post Code")]
        [StringLength(10)]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Please enter your country")]
        [Display(Name = "Country")]
        [StringLength(50)]
        public string Country { get; set; }
        
    }
}
