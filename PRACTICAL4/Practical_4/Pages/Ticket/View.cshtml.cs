using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practical_4.Models;

namespace Practical_4.Pages.Ticket
{
    public class ViewModel : PageModel
    {
        public bool IsTicketFound { get; set; } = false;
        public TicketPass? Ticket { get; set; }

        public IActionResult OnGet([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                IsTicketFound = false;
                return Page();
            }

            var foundTicket = TicketStore.GetTicket(id);
            if (foundTicket == null)
            {
                IsTicketFound = false;
                return Page();
            }

            IsTicketFound = true;
            Ticket = foundTicket;
            return Page();
        }
    }
}
