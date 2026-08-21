using System;

namespace AcademicLeaveManagement.Models
{
    /// <summary>
    /// Represents a student's leave application in the academic portal.
    /// </summary>
    [Serializable]
    public class LeaveApplication
    {
        /// <summary>
        /// Gets or sets the unique identifier for the leave application (e.g., LV-2026-1001).
        /// </summary>
        public string LeaveId { get; set; }

        /// <summary>
        /// Gets or sets the username of the student who applied.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the category/type of leave (e.g., Casual, Medical, Duty).
        /// </summary>
        public string LeaveType { get; set; }

        /// <summary>
        /// Gets or sets the start date of the requested leave period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the requested leave period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the leave duration or session mode (e.g., Full Day, Half Day Morning).
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the detailed justification provided by the student.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets any supporting document checklist or additional remarks.
        /// </summary>
        public string SupportingInformation { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the leave was submitted.
        /// </summary>
        public DateTime AppliedDate { get; set; }

        /// <summary>
        /// Gets or sets the current workflow status (Pending, Approved, Rejected).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveApplication"/> class.
        /// </summary>
        public LeaveApplication()
        {
            Status = "Pending";
            AppliedDate = DateTime.Now;
        }
    }
}
