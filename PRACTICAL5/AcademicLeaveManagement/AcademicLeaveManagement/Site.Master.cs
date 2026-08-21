using System;
using System.Web.UI;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Master Page code-behind providing common navigation, authentication checks, and layout logic.
    /// </summary>
    public partial class SiteMaster : MasterPage
    {
        /// <summary>
        /// Handles the Page_Load event of the master page.
        /// Verifies session authentication state and displays the logged-in student's username.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verify if user is logged in
            if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblLoggedUser.Text = Session["Username"].ToString();
                }
                HighlightActiveNavigation();
            }
        }

        /// <summary>
        /// Sets the active CSS class on the current page's navigation link.
        /// </summary>
        private void HighlightActiveNavigation()
        {
            string currentPath = Request.AppRelativeCurrentExecutionFilePath;

            if (currentPath.EndsWith("Dashboard.aspx", StringComparison.OrdinalIgnoreCase))
                lnkDashboard.CssClass += " active";
            else if (currentPath.EndsWith("AcademicCalendar.aspx", StringComparison.OrdinalIgnoreCase))
                lnkCalendar.CssClass += " active";
            else if (currentPath.EndsWith("ApplyLeave.aspx", StringComparison.OrdinalIgnoreCase))
                lnkApplyLeave.CssClass += " active";
            else if (currentPath.EndsWith("LeaveHistory.aspx", StringComparison.OrdinalIgnoreCase))
                lnkLeaveHistory.CssClass += " active";
        }

        /// <summary>
        /// Handles the Sign Out click event.
        /// Clears and abandons the ASP.NET session state, then redirects to Login.aspx.
        /// Note: Persistent cookies (e.g. Remember Me) remain intact across sessions unless explicitly removed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            // Abandon current session state
            Session.Abandon();

            // Redirect to Login page with status query parameter
            Response.Redirect("~/Login.aspx?status=loggedout");
        }
    }
}
