using System;
using System.ComponentModel.DataAnnotations;

namespace Practical_4.Models
{
    public class EventRegistration
    {
        // --- STUDENT INFORMATION ---

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name must contain only alphabetic characters and spaces.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enrollment Number is required.")]
        [RegularExpression(@"^[A-Z0-9]{8,15}$", ErrorMessage = "Enrollment Number must consist of 8 to 15 uppercase letters and numbers without special characters.")]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student ID (Roll Number) is required.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Student ID must contain numeric values only.")]
        [Display(Name = "Student ID / Roll Number")]
        public string StudentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Class is required.")]
        [Display(Name = "Class")]
        public string Class { get; set; } = string.Empty;

        [Required(ErrorMessage = "Division is required.")]
        [Display(Name = "Division")]
        public string Division { get; set; } = string.Empty;

        [Required(ErrorMessage = "College Name is required.")]
        [Display(Name = "College Name")]
        public string CollegeName { get; set; } = "Marwadi University";

        [Required(ErrorMessage = "Department selection is required.")]
        [Display(Name = "Department")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address format.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your Email Address.")]
        [Compare("Email", ErrorMessage = "Email Address and Confirm Email do not match.")]
        [Display(Name = "Confirm Email Address")]
        public string ConfirmEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must contain exactly 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        // --- EVENT TYPE ---

        [Required(ErrorMessage = "Please select an Event Category (Technical or Non-Technical).")]
        [Display(Name = "Event Category")]
        public string EventType { get; set; } = "Technical";

        // --- TECHNICAL EVENT FIELDS ---

        [Display(Name = "Technical Sub-Event")]
        public string? TechnicalEvent { get; set; }

        [Display(Name = "Tech Stack")]
        public string? TechStack { get; set; }

        [Range(1, 10, ErrorMessage = "Team Size must be between 1 and 10 members.")]
        [Display(Name = "Team Size")]
        public int TeamSize { get; set; } = 1;

        [Url(ErrorMessage = "Please enter a valid URL format (e.g., https://github.com/username).")]
        [Display(Name = "GitHub Profile")]
        public string? GithubProfile { get; set; }

        // --- NON-TECHNICAL EVENT FIELDS ---

        [Display(Name = "Non-Technical Sub-Event")]
        public string? NonTechnicalEvent { get; set; }

        [Display(Name = "Squad Name")]
        public string? SquadName { get; set; }

        [Display(Name = "Equipment Needed")]
        public string? EquipmentNeeded { get; set; }

        // --- LOGISTICS ---

        [Required(ErrorMessage = "Please select your T-Shirt Size.")]
        [Display(Name = "T-Shirt Size")]
        public string TShirtSize { get; set; } = string.Empty;

        [Display(Name = "I agree to the Terms & Conditions")]
        public bool AgreedToTerms { get; set; }
    }
}
