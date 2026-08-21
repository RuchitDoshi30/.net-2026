using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademicLeaveManagement.Models;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Code-behind for Leave History and Tracking.
    /// Demonstrates data-binding with GridView and DetailsView, row selection event handling,
    /// dynamic filtering, and in-memory Session state workflow management.
    /// </summary>
    public partial class LeaveHistory : Page
    {
        /// <summary>
        /// Handles the Page_Load event.
        /// Authenticates the user session and binds the leave history GridView.
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
                BindGridView();
            }
        }

        /// <summary>
        /// Fetches leave applications from Session state and binds them to the GridView control.
        /// </summary>
        private void BindGridView()
        {
            var leaves = Session["LeaveApplications"] as List<LeaveApplication> ?? new List<LeaveApplication>();
            string filter = ddlFilterStatus.SelectedValue;

            IEnumerable<LeaveApplication> query = leaves;

            if (!string.IsNullOrEmpty(filter) && filter != "ALL")
            {
                query = query.Where(l => string.Equals(l.Status, filter, StringComparison.OrdinalIgnoreCase));
            }

            var result = query.OrderByDescending(l => l.AppliedDate).ToList();

            gvLeaveHistory.DataSource = result;
            gvLeaveHistory.DataBind();
        }

        /// <summary>
        /// Handles row selection in the GridView control.
        /// Extracts the selected leave record and binds it to the DetailsView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void gvLeaveHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvLeaveHistory.SelectedDataKey != null)
            {
                string selectedId = gvLeaveHistory.SelectedDataKey.Value.ToString();
                DisplaySelectedDetails(selectedId);
            }
        }

        /// <summary>
        /// Displays the full details of a specific leave application in the DetailsView control.
        /// </summary>
        /// <param name="leaveId">The unique identifier of the leave application.</param>
        private void DisplaySelectedDetails(string leaveId)
        {
            var leaves = Session["LeaveApplications"] as List<LeaveApplication> ?? new List<LeaveApplication>();
            var target = leaves.FirstOrDefault(l => string.Equals(l.LeaveId, leaveId, StringComparison.OrdinalIgnoreCase));

            if (target != null)
            {
                pnlDetails.Visible = true;
                dvLeaveDetails.DataSource = new List<LeaveApplication> { target };
                dvLeaveDetails.DataBind();
            }
            else
            {
                pnlDetails.Visible = false;
            }
        }

        /// <summary>
        /// Handles the status filter dropdown change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void ddlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvLeaveHistory.SelectedIndex = -1;
            pnlDetails.Visible = false;
            BindGridView();
        }

        /// <summary>
        /// Closes the DetailsView inspection panel.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            gvLeaveHistory.SelectedIndex = -1;
        }

        /// <summary>
        /// Updates the status of the currently selected leave application in Session state.
        /// </summary>
        /// <param name="newStatus">The new workflow status (Approved, Rejected, Pending).</param>
        private void UpdateSelectedLeaveStatus(string newStatus)
        {
            if (gvLeaveHistory.SelectedDataKey != null)
            {
                string selectedId = gvLeaveHistory.SelectedDataKey.Value.ToString();
                var leaves = Session["LeaveApplications"] as List<LeaveApplication>;

                if (leaves != null)
                {
                    var target = leaves.FirstOrDefault(l => string.Equals(l.LeaveId, selectedId, StringComparison.OrdinalIgnoreCase));
                    if (target != null)
                    {
                        target.Status = newStatus;
                        Session["LeaveApplications"] = leaves;

                        // Rebind UI
                        BindGridView();
                        DisplaySelectedDetails(selectedId);

                        lblHistoryMessage.Text = string.Format(
                            "<div class='alert-box alert-success'>✅ Application <strong>{0}</strong> status has been updated to <strong>{1}</strong> in current Session.</div>",
                            selectedId,
                            newStatus
                        );
                        return;
                    }
                }
            }

            lblHistoryMessage.Text = "<div class='alert-box alert-danger'>❌ Please select a leave application first.</div>";
        }

        /// <summary>
        /// Simulates approving the selected leave application.
        /// </summary>
        protected void btnSimulateApprove_Click(object sender, EventArgs e)
        {
            UpdateSelectedLeaveStatus("Approved");
        }

        /// <summary>
        /// Simulates rejecting the selected leave application.
        /// </summary>
        protected void btnSimulateReject_Click(object sender, EventArgs e)
        {
            UpdateSelectedLeaveStatus("Rejected");
        }

        /// <summary>
        /// Resets the selected leave application status to Pending.
        /// </summary>
        protected void btnSimulatePending_Click(object sender, EventArgs e)
        {
            UpdateSelectedLeaveStatus("Pending");
        }

        /// <summary>
        /// Returns the badge CSS class corresponding to a given status string.
        /// </summary>
        /// <param name="status">The leave application status.</param>
        /// <returns>CSS class string.</returns>
        public string GetStatusBadgeCss(string status)
        {
            if (string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
                return "badge badge-approved";
            if (string.Equals(status, "Rejected", StringComparison.OrdinalIgnoreCase))
                return "badge badge-rejected";
            return "badge badge-pending";
        }
    }
}
