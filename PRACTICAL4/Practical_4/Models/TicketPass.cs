using System;
using System.Collections.Concurrent;

namespace Practical_4.Models
{
    public class TicketPass
    {
        public string PassId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EnrollmentNumber { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string CollegeName { get; set; } = "Marwadi University";
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string EventType { get; set; } = "Technical";
        public int TeamSize { get; set; } = 1;
        public string? TechStack { get; set; }
        public string? GithubProfile { get; set; }
        public string? SquadName { get; set; }
        public string? EquipmentNeeded { get; set; }
        public string TShirtSize { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = new DateTime(2026, 10, 24);
        public string Venue { get; set; } = "Main Campus Auditorium & Tech Hub";
        public double FeeAmount { get; set; } = 0;
        public string RegistrationStatus { get; set; } = "VERIFIED & ACTIVE";
        public string TicketUrl { get; set; } = string.Empty;
        public string QrCodeBase64 { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class TicketStore
    {
        private static readonly ConcurrentDictionary<string, TicketPass> Tickets = new(StringComparer.OrdinalIgnoreCase);

        public static void SaveTicket(TicketPass ticket)
        {
            if (ticket != null && !string.IsNullOrEmpty(ticket.PassId))
            {
                Tickets[ticket.PassId] = ticket;
            }
        }

        public static TicketPass? GetTicket(string passId)
        {
            if (string.IsNullOrWhiteSpace(passId)) return null;
            Tickets.TryGetValue(passId, out var ticket);
            return ticket;
        }
    }
}
