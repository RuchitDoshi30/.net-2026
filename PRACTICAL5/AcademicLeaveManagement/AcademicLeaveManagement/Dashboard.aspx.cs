using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademicLeaveManagement.Models;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Code-behind for the Student Dashboard.
    /// Demonstrates Session state consumption, KPI calculations, and data-binding to GridView and ListBox controls.
    /// </summary>
    public partial class Dashboard : Page
    {
        /// <summary>
        /// Handles the Page_Load event of the dashboard.
        /// Authenticates the user and binds statistical metrics and upcoming events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Security verification: redirect unauthenticated sessions to Login
            if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                PopulateDashboardData();
            }
        }

        /// <summary>
        /// Loads student details, calculates leave metrics from Session, and binds controls.
        /// </summary>
        private void PopulateDashboardData()
        {
            // Display username from Session
            if (Session["Username"] != null)
            {
                lblStudentName.Text = Session["Username"].ToString();
            }

            // Retrieve Leave Applications from Session
            var leaves = Session["LeaveApplications"] as List<LeaveApplication> ?? new List<LeaveApplication>();

            // Calculate KPI counts
            lblTotalLeaves.Text = leaves.Count.ToString();
            lblPendingLeaves.Text = leaves.Count(l => string.Equals(l.Status, "Pending", StringComparison.OrdinalIgnoreCase)).ToString();
            lblApprovedLeaves.Text = leaves.Count(l => string.Equals(l.Status, "Approved", StringComparison.OrdinalIgnoreCase)).ToString();
            lblRejectedLeaves.Text = leaves.Count(l => string.Equals(l.Status, "Rejected", StringComparison.OrdinalIgnoreCase)).ToString();

            // Bind Recent Leaves GridView (Take top 5 ordered by application date descending)
            gvRecentLeaves.DataSource = leaves.OrderByDescending(l => l.AppliedDate).Take(5).ToList();
            gvRecentLeaves.DataBind();

            // Bind Upcoming Academic Events ListBox
            var upcomingEvents = AcademicEventRepository.GetUpcomingEvents();
            lstUpcomingEvents.Items.Clear();
            foreach (var evt in upcomingEvents)
            {
                string displayText = string.Format("{0:MMM dd} - {1} ({2})", evt.EventDate, evt.EventName, evt.EventType);
                ListItem item = new ListItem(displayText, evt.EventDate.ToString("yyyy-MM-dd"));
                lstUpcomingEvents.Items.Add(item);
            }
        }

        /// <summary>
        /// Returns the appropriate CSS badge class based on the leave status string.
        /// </summary>
        /// <param name="status">The leave application status.</param>
        /// <returns>CSS class string for badge styling.</returns>
        public string GetStatusBadgeCss(string status)
        {
            if (string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
                return "badge badge-approved";
            if (string.Equals(status, "Rejected", StringComparison.OrdinalIgnoreCase))
                return "badge badge-rejected";
            return "badge badge-pending";
        }

        /// <summary>
        /// Handles the click event to open the Academic Calendar with the selected event date.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void btnJumpToCalendar_Click(object sender, EventArgs e)
        {
            if (lstUpcomingEvents.SelectedIndex >= 0)
            {
                string selectedDate = lstUpcomingEvents.SelectedValue;
                Response.Redirect("~/AcademicCalendar.aspx?date=" + Server.UrlEncode(selectedDate));
            }
            else
            {
                Response.Redirect("~/AcademicCalendar.aspx");
            }
        }
    }
}
