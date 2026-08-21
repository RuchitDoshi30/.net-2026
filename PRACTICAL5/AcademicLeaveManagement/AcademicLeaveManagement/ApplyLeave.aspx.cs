using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademicLeaveManagement.Models;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Code-behind for the Apply Leave form.
    /// Demonstrates ASP.NET Rich Controls (DropDownList, RadioButtonList, CheckBoxList),
    /// Server-Side Validation (RequiredFieldValidator, CustomValidator), and Session State persistence.
    /// </summary>
    public partial class ApplyLeave : Page
    {
        /// <summary>
        /// Handles the Page_Load event.
        /// Authenticates the student session and initializes form defaults.
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
                if (Session["Username"] != null)
                {
                    txtStudentUsername.Text = Session["Username"].ToString();
                }

                // Check if user came from Calendar with a preselected date
                if (!string.IsNullOrEmpty(Request.QueryString["start"]))
                {
                    DateTime selectedDate;
                    if (DateTime.TryParse(Request.QueryString["start"], out selectedDate))
                    {
                        txtStartDate.Text = selectedDate.ToString("yyyy-MM-dd");
                        txtEndDate.Text = selectedDate.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    // Default to today's date
                    txtStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
        }

        /// <summary>
        /// Server-side validation logic for date range and reason length checks.
        /// </summary>
        /// <param name="source">The source CustomValidator control.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> containing validation parameters.</param>
        protected void cvDateRange_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime startDate;
            DateTime endDate;

            bool isStartValid = DateTime.TryParse(txtStartDate.Text, out startDate);
            bool isEndValid = DateTime.TryParse(txtEndDate.Text, out endDate);

            if (!isStartValid || !isEndValid)
            {
                cvDateRange.ErrorMessage = "Please provide valid start and end dates.";
                args.IsValid = false;
                return;
            }

            if (endDate.Date < startDate.Date)
            {
                cvDateRange.ErrorMessage = "End Date cannot be earlier than Start Date.";
                args.IsValid = false;
                return;
            }

            if (txtReason.Text.Trim().Length < 10)
            {
                cvDateRange.ErrorMessage = "Please provide a detailed reason with at least 10 characters.";
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        /// <summary>
        /// Handles the submission of a new leave application.
        /// Gathers form parameters, constructs a <see cref="LeaveApplication"/> instance, and stores it in Session state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            // Retrieve existing leave collection from Session or create a new list
            var leaves = Session["LeaveApplications"] as List<LeaveApplication>;
            if (leaves == null)
            {
                leaves = new List<LeaveApplication>();
            }

            // Generate unique Leave ID
            string leaveId = string.Format("LV-2026-{0}", 1000 + leaves.Count + 1);

            // Collect selected supporting documents
            StringBuilder supportingDocs = new StringBuilder();
            foreach (ListItem item in cblSupportingDocs.Items)
            {
                if (item.Selected)
                {
                    if (supportingDocs.Length > 0)
                    {
                        supportingDocs.Append("; ");
                    }
                    supportingDocs.Append(item.Value);
                }
            }

            if (supportingDocs.Length == 0)
            {
                supportingDocs.Append("None attached / Self-Declaration");
            }

            // Build LeaveApplication object
            LeaveApplication newLeave = new LeaveApplication
            {
                LeaveId = leaveId,
                Username = Session["Username"] != null ? Session["Username"].ToString() : "student",
                LeaveType = ddlLeaveType.SelectedValue,
                StartDate = DateTime.Parse(txtStartDate.Text),
                EndDate = DateTime.Parse(txtEndDate.Text),
                Duration = rblDuration.SelectedValue,
                Reason = txtReason.Text.Trim(),
                SupportingInformation = supportingDocs.ToString(),
                AppliedDate = DateTime.Now,
                Status = "Pending"
            };

            // Save in Session state
            leaves.Add(newLeave);
            Session["LeaveApplications"] = leaves;

            // Display success notification
            lblStatusMessage.Text = string.Format(
                "<div class='alert-box alert-success'>🎉 <strong>Success!</strong> Leave application <strong>{0}</strong> has been submitted and stored in your current Session. Status: <span class='badge badge-pending'>Pending</span>. <a href='LeaveHistory.aspx' style='color:#065f46; font-weight:700; text-decoration:underline; margin-left:0.5rem;'>View in Leave History &rarr;</a></div>",
                leaveId
            );

            // Reset form inputs
            ClearForm();
        }

        /// <summary>
        /// Handles the Reset button click to restore form inputs to default states.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblStatusMessage.Text = string.Empty;
        }

        /// <summary>
        /// Resets all input controls to their default state.
        /// </summary>
        private void ClearForm()
        {
            ddlLeaveType.SelectedIndex = 0;
            txtStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            rblDuration.SelectedIndex = 0;
            txtReason.Text = string.Empty;
            cblSupportingDocs.ClearSelection();
        }
    }
}
