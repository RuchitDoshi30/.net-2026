using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practical_4.Models;
using QRCoder;

namespace Practical_4.Pages
{
    public class IndexModel : PageModel
    {
        private static readonly HashSet<string> RegisteredEnrollments = new(StringComparer.OrdinalIgnoreCase);

        [BindProperty]
        public EventRegistration Registration { get; set; } = new EventRegistration();

        public bool IsRegistered { get; set; } = false;
        public double RegistrationFee { get; set; } = 0;
        public string RegistrationId { get; set; } = string.Empty;
        public string SelectedSubEventName { get; set; } = string.Empty;
        public string VenueLocation { get; set; } = "Main Campus Auditorium & Tech Hub";
        public TicketPass? GeneratedTicket { get; set; }

        public void OnGet()
        {
            IsRegistered = false;
        }

        public IActionResult OnPost()
        {
            // 1. Trim whitespace from all text inputs
            TrimInputs();

            // 2. Explicit Validation for Terms & Conditions Checkbox
            if (!Registration.AgreedToTerms)
            {
                ModelState.AddModelError("Registration.AgreedToTerms", "You must agree to the Terms & Conditions to complete registration.");
            }

            // 3. Apply Conditional Category Validation & Remove Irrelevant Category State
            if (Registration.EventType == "Technical")
            {
                ModelState.Remove("Registration.NonTechnicalEvent");
                ModelState.Remove("Registration.SquadName");
                ModelState.Remove("Registration.EquipmentNeeded");

                if (string.IsNullOrWhiteSpace(Registration.TechnicalEvent))
                {
                    ModelState.AddModelError("Registration.TechnicalEvent", "Technical Sub-Event is required.");
                }

                if (string.IsNullOrWhiteSpace(Registration.TechStack))
                {
                    ModelState.AddModelError("Registration.TechStack", "Tech Stack is required.");
                }
                else if (Registration.TechStack.Length < 2 || Registration.TechStack.Length > 200)
                {
                    ModelState.AddModelError("Registration.TechStack", "Tech Stack must be between 2 and 200 characters.");
                }

                if (Registration.TeamSize < 1 || Registration.TeamSize > 10)
                {
                    ModelState.AddModelError("Registration.TeamSize", "Team Size must be between 1 and 10 members.");
                }
            }
            else if (Registration.EventType == "Non-Technical")
            {
                ModelState.Remove("Registration.TechnicalEvent");
                ModelState.Remove("Registration.TechStack");
                ModelState.Remove("Registration.TeamSize");
                ModelState.Remove("Registration.GithubProfile");

                if (string.IsNullOrWhiteSpace(Registration.NonTechnicalEvent))
                {
                    ModelState.AddModelError("Registration.NonTechnicalEvent", "Non-Technical Sub-Event is required.");
                }

                if (string.IsNullOrWhiteSpace(Registration.SquadName))
                {
                    ModelState.AddModelError("Registration.SquadName", "Squad Name is required.");
                }
                else if (Registration.SquadName.Length < 3 || Registration.SquadName.Length > 50)
                {
                    ModelState.AddModelError("Registration.SquadName", "Squad Name must be between 3 and 50 characters.");
                }

                if (!string.IsNullOrEmpty(Registration.EquipmentNeeded) && Registration.EquipmentNeeded.Length > 500)
                {
                    ModelState.AddModelError("Registration.EquipmentNeeded", "Equipment Needed description cannot exceed 500 characters.");
                }
            }

            // 4. Duplicate Registration Check
            if (!string.IsNullOrWhiteSpace(Registration.EnrollmentNumber))
            {
                lock (RegisteredEnrollments)
                {
                    if (RegisteredEnrollments.Contains(Registration.EnrollmentNumber))
                    {
                        ModelState.AddModelError("Registration.EnrollmentNumber", "This Enrollment Number is already registered for an event.");
                    }
                }
            }

            // 5. Validate Model State
            if (!ModelState.IsValid)
            {
                IsRegistered = false;
                return Page();
            }

            // 6. Calculate Registration Fee & Assign Sub-Event Name
            if (Registration.EventType == "Technical")
            {
                SelectedSubEventName = Registration.TechnicalEvent ?? "Technical Event";
                RegistrationFee = Registration.TechnicalEvent switch
                {
                    "Hackathon" => 350,
                    "Coding Competition" => 250,
                    "Web Development Challenge" => 300,
                    "Robotics Competition" => 400,
                    "UI/UX Design Competition" => 250,
                    _ => 250
                };
            }
            else
            {
                SelectedSubEventName = Registration.NonTechnicalEvent ?? "Non-Technical Event";
                RegistrationFee = Registration.NonTechnicalEvent switch
                {
                    "Dance Competition" => 300,
                    "Singing Competition" => 250,
                    "Photography Competition" => 200,
                    "Drama Competition" => 350,
                    "Quiz Competition" => 150,
                    _ => 200
                };
            }

            // 7. Generate Unique Pass Code in Format: CAMPUS-2026-XXXXXX
            RegistrationId = "CAMPUS-2026-" + Random.Shared.Next(100000, 999999);

            // 8. Generate Ticket URL and Dynamic PNG Base64 QR Code using QRCoder (ECC Level Q)
            string ticketUrl = $"{Request.Scheme}://{Request.Host}/Ticket/View?id={RegistrationId}";
            string qrCodeBase64 = string.Empty;

            using (var qrGenerator = new QRCodeGenerator())
            {
                using (var qrCodeData = qrGenerator.CreateQrCode(ticketUrl, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new PngByteQRCode(qrCodeData))
                    {
                        byte[] qrCodeBytes = qrCode.GetGraphic(20);
                        qrCodeBase64 = "data:image/png;base64," + Convert.ToBase64String(qrCodeBytes);
                    }
                }
            }

            // 9. Store ticket in thread-safe repository
            var ticket = new TicketPass
            {
                PassId = RegistrationId,
                FullName = Registration.FullName,
                EnrollmentNumber = Registration.EnrollmentNumber,
                StudentId = Registration.StudentId,
                Class = Registration.Class,
                Division = Registration.Division,
                Department = Registration.Department,
                CollegeName = Registration.CollegeName,
                Email = Registration.Email,
                PhoneNumber = Registration.PhoneNumber,
                EventName = SelectedSubEventName,
                EventType = Registration.EventType,
                TeamSize = Registration.TeamSize,
                TechStack = Registration.TechStack,
                GithubProfile = Registration.GithubProfile,
                SquadName = Registration.SquadName,
                EquipmentNeeded = Registration.EquipmentNeeded,
                TShirtSize = Registration.TShirtSize,
                FeeAmount = RegistrationFee,
                Venue = VenueLocation,
                TicketUrl = ticketUrl,
                QrCodeBase64 = qrCodeBase64,
                RegistrationStatus = "VERIFIED & ACTIVE"
            };

            TicketStore.SaveTicket(ticket);
            GeneratedTicket = ticket;

            lock (RegisteredEnrollments)
            {
                RegisteredEnrollments.Add(Registration.EnrollmentNumber);
            }

            IsRegistered = true;
            return Page();
        }

        private void TrimInputs()
        {
            if (Registration != null)
            {
                Registration.FullName = Registration.FullName?.Trim() ?? string.Empty;
                Registration.EnrollmentNumber = Registration.EnrollmentNumber?.Trim().ToUpperInvariant() ?? string.Empty;
                Registration.StudentId = Registration.StudentId?.Trim() ?? string.Empty;
                Registration.Class = Registration.Class?.Trim() ?? string.Empty;
                Registration.Division = Registration.Division?.Trim() ?? string.Empty;
                Registration.CollegeName = Registration.CollegeName?.Trim() ?? string.Empty;
                Registration.Department = Registration.Department?.Trim() ?? string.Empty;
                Registration.Email = Registration.Email?.Trim() ?? string.Empty;
                Registration.ConfirmEmail = Registration.ConfirmEmail?.Trim() ?? string.Empty;
                Registration.PhoneNumber = Registration.PhoneNumber?.Trim() ?? string.Empty;
                Registration.EventType = Registration.EventType?.Trim() ?? "Technical";
                Registration.TechnicalEvent = Registration.TechnicalEvent?.Trim();
                Registration.TechStack = Registration.TechStack?.Trim();
                Registration.GithubProfile = Registration.GithubProfile?.Trim();
                Registration.NonTechnicalEvent = Registration.NonTechnicalEvent?.Trim();
                Registration.SquadName = Registration.SquadName?.Trim();
                Registration.EquipmentNeeded = Registration.EquipmentNeeded?.Trim();
                Registration.TShirtSize = Registration.TShirtSize?.Trim() ?? string.Empty;
            }
        }
    }
}
