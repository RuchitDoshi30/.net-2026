using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademicLeaveManagement.Models;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Code-behind for the Academic Calendar page.
    /// Demonstrates the ASP.NET &lt;asp:Calendar&gt; Rich Web Server Control,
    /// dynamic DayRender customization, and interactive event selection handling.
    /// </summary>
    public partial class AcademicCalendar : Page
    {
        /// <summary>
        /// Handles the Page_Load event.
        /// Authenticates session, initializes the calendar selection, and binds event directories.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                PopulateEventsList();

                DateTime targetDate = DateTime.Today;
                if (!string.IsNullOrEmpty(Request.QueryString["date"]))
                {
                    DateTime parsedDate;
                    if (DateTime.TryParse(Request.QueryString["date"], out parsedDate))
                    {
                        targetDate = parsedDate;
                    }
                }

                calAcademic.SelectedDate = targetDate;
                calAcademic.VisibleDate = targetDate;
                DisplayEventDetails(targetDate);
            }
        }

        /// <summary>
        /// Binds all semester academic events to the ListBox control.
        /// </summary>
        private void PopulateEventsList()
        {
            var allEvents = AcademicEventRepository.GetAllEvents();
            lstAllEvents.Items.Clear();

            foreach (var evt in allEvents)
            {
                string displayText = string.Format("{0:yyyy-MM-dd} : {1} ({2})", evt.EventDate, evt.EventName, evt.EventType);
                ListItem item = new ListItem(displayText, evt.EventDate.ToString("yyyy-MM-dd"));
                lstAllEvents.Items.Add(item);
            }
        }

        /// <summary>
        /// Handles the DayRender event of the Calendar control.
        /// Customizes the visual presentation of days containing academic events, holidays, or examinations.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DayRenderEventArgs"/> instance containing cell and day data.</param>
        protected void calAcademic_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime date = e.Day.Date;
            AcademicEvent evt = AcademicEventRepository.GetEventByDate(date);

            if (evt != null)
            {
                // Inject custom tooltip
                e.Cell.ToolTip = string.Format("{0} [{1}]: {2}", evt.EventName, evt.EventType, evt.Description);

                // Add visual badge inside the calendar cell
                string badgeHtml = string.Format(
                    "<span class='calendar-badge' style='background-color:{0};' title='{1}'>{2}</span>",
                    evt.BadgeColor,
                    Server.HtmlEncode(evt.Description),
                    Server.HtmlEncode(evt.EventName)
                );

                e.Cell.Controls.Add(new LiteralControl(badgeHtml));

                // Customize cell border based on event type
                if (evt.EventType == "Exam")
                {
                    e.Cell.Style.Add("background-color", "#fff1f2");
                    e.Cell.Style.Add("border", "1.5px solid #f87171");
                }
                else if (evt.EventType == "Holiday")
                {
                    e.Cell.Style.Add("background-color", "#f0fdf4");
                    e.Cell.Style.Add("border", "1.5px solid #86efac");
                }
                else if (evt.EventType == "Practical")
                {
                    e.Cell.Style.Add("background-color", "#faf5ff");
                    e.Cell.Style.Add("border", "1.5px solid #c084fc");
                }
                else if (evt.EventType == "Submission")
                {
                    e.Cell.Style.Add("background-color", "#fffbeb");
                    e.Cell.Style.Add("border", "1.5px solid #fcd34d");
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the Calendar control.
        /// Displays details of the selected date to the student.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void calAcademic_SelectionChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = calAcademic.SelectedDate;
            DisplayEventDetails(selectedDate);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event on the event directory ListBox.
        /// Adjusts the calendar to view and select the selected event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void lstAllEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAllEvents.SelectedIndex >= 0)
            {
                DateTime selectedDate;
                if (DateTime.TryParse(lstAllEvents.SelectedValue, out selectedDate))
                {
                    calAcademic.SelectedDate = selectedDate;
                    calAcademic.VisibleDate = selectedDate;
                    DisplayEventDetails(selectedDate);
                }
            }
        }

        /// <summary>
        /// Renders detailed event particulars on the UI panel for a specified date.
        /// </summary>
        /// <param name="date">The target date to display.</param>
        private void DisplayEventDetails(DateTime date)
        {
            lblSelectedDate.Text = date.ToString("dddd, MMMM dd, yyyy");
            AcademicEvent evt = AcademicEventRepository.GetEventByDate(date);

            if (evt != null)
            {
                phEventContent.Visible = true;
                phNoEvent.Visible = false;

                lblEventName.Text = evt.EventName;
                lblEventDescription.Text = evt.Description;
                lblEventTypeBadge.Text = evt.EventType;
                lblEventTypeBadge.CssClass = "badge " + GetBadgeClassForType(evt.EventType);
            }
            else
            {
                phEventContent.Visible = false;
                phNoEvent.Visible = true;
            }

            lnkApplyForSelectedDate.NavigateUrl = string.Format("~/ApplyLeave.aspx?start={0}", date.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// Returns the CSS badge class for a given event type.
        /// </summary>
        /// <param name="type">The category of the event.</param>
        /// <returns>CSS class string.</returns>
        private string GetBadgeClassForType(string type)
        {
            switch (type?.ToLower())
            {
                case "exam": return "badge-exam";
                case "holiday": return "badge-holiday";
                case "practical": return "badge-practical";
                case "submission": return "badge-submission";
                default: return "badge-milestone";
            }
        }
    }
}
