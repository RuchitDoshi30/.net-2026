using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using AcademicLeaveManagement.Models;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Code-behind for the Student Login page.
    /// Demonstrates ASP.NET Web Forms authentication, Session variable initialization,
    /// and HTTP Cookie creation and persistence for "Remember Me" functionality.
    /// </summary>
    public partial class Login : Page
    {
        private const string REMEMBER_COOKIE_NAME = "StudentUsername";

        /// <summary>
        /// Handles the Page_Load event.
        /// Checks for existing Remember Me cookies to prefill login fields and inspects logout query strings.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user just logged out
                if (Request.QueryString["status"] == "loggedout")
                {
                    lblMessage.Text = "<div class='alert-box alert-info'>ℹ️ You have been securely logged out. Session state cleared.</div>";
                }

                // If already logged in, directly redirect to Dashboard
                if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
                {
                    Response.Redirect("~/Dashboard.aspx");
                    return;
                }

                // =========================================================================
                // COOKIE DEMONSTRATION: Reading persistent cookie on page load
                // =========================================================================
                HttpCookie rememberCookie = Request.Cookies[REMEMBER_COOKIE_NAME];
                if (rememberCookie != null && !string.IsNullOrWhiteSpace(rememberCookie.Value))
                {
                    txtUsername.Text = Server.HtmlEncode(rememberCookie.Value);
                    chkRememberMe.Checked = true;
                }
            }
        }

        /// <summary>
        /// Handles the Login button click event.
        /// Performs credential validation, creates Session state variables, and updates persistent Cookies.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Demonstration authentication check
            if (string.Equals(username, "student", StringComparison.OrdinalIgnoreCase) && password == "12345")
            {
                // =========================================================================
                // SESSION STATE DEMONSTRATION: Storing login state & student particulars
                // =========================================================================
                Session["Username"] = username;
                Session["IsLoggedIn"] = true;

                // Initialize sample leave applications in session if not already existing
                if (Session["LeaveApplications"] == null)
                {
                    InitializeSeedLeaveApplications(username);
                }

                // =========================================================================
                // COOKIE DEMONSTRATION: Writing persistent cookie when "Remember Me" is checked
                // =========================================================================
                if (chkRememberMe.Checked)
                {
                    HttpCookie cookie = new HttpCookie(REMEMBER_COOKIE_NAME, username)
                    {
                        Expires = DateTime.Now.AddDays(15),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    // If unchecked, remove/expire any existing Remember Me cookie
                    if (Request.Cookies[REMEMBER_COOKIE_NAME] != null)
                    {
                        HttpCookie expiredCookie = new HttpCookie(REMEMBER_COOKIE_NAME, string.Empty)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            HttpOnly = true
                        };
                        Response.Cookies.Add(expiredCookie);
                    }
                }

                // Redirect authenticated student to Dashboard
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "<div class='alert-box alert-danger'>❌ Invalid credentials! Use username: <strong>student</strong> and password: <strong>12345</strong>.</div>";
            }
        }

        /// <summary>
        /// Seeds initial demonstration leave applications into the student's session.
        /// </summary>
        /// <param name="username">The logged-in student's username.</param>
        private void InitializeSeedLeaveApplications(string username)
        {
            DateTime today = DateTime.Today;

            var list = new List<LeaveApplication>
            {
                new LeaveApplication
                {
                    LeaveId = "LV-2026-1001",
                    Username = username,
                    LeaveType = "Medical Leave",
                    StartDate = today.AddDays(-10),
                    EndDate = today.AddDays(-9),
                    Duration = "Full Day",
                    Reason = "Diagnosed with acute viral fever. Doctor advised 2 days complete bed rest.",
                    SupportingInformation = "Medical Certificate / Doctor's Prescription attached",
                    AppliedDate = today.AddDays(-11),
                    Status = "Approved"
                },
                new LeaveApplication
                {
                    LeaveId = "LV-2026-1002",
                    Username = username,
                    LeaveType = "Casual Leave",
                    StartDate = today.AddDays(-3),
                    EndDate = today.AddDays(-3),
                    Duration = "Half Day (Morning Session)",
                    Reason = "Attending family function and municipal document verification.",
                    SupportingInformation = "Parent / Guardian Acknowledgment Note",
                    AppliedDate = today.AddDays(-4),
                    Status = "Rejected"
                },
                new LeaveApplication
                {
                    LeaveId = "LV-2026-1003",
                    Username = username,
                    LeaveType = "Academic Duty / OD",
                    StartDate = today.AddDays(5),
                    EndDate = today.AddDays(6),
                    Duration = "Full Day",
                    Reason = "Representing college at State Level .NET Hackathon and Project Expo.",
                    SupportingInformation = "Official Duty / Sports Participation Letter; HOD / Faculty Advisor Recommendation",
                    AppliedDate = today.AddDays(-1),
                    Status = "Pending"
                }
            };

            Session["LeaveApplications"] = list;
        }
    }
}
