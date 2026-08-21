using System;

namespace AcademicLeaveManagement.Models
{
    /// <summary>
    /// Represents an academic event, examination, or institutional holiday on the college calendar.
    /// </summary>
    [Serializable]
    public class AcademicEvent
    {
        /// <summary>
        /// Gets or sets the unique numerical identifier for the event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date of the academic event.
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets or sets the title or name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the descriptive details regarding the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category of event (e.g., Exam, Holiday, Milestone, Practical, Submission).
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the visual badge color or CSS class used for calendar rendering.
        /// </summary>
        public string BadgeColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicEvent"/> class.
        /// </summary>
        public AcademicEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicEvent"/> class with specific parameters.
        /// </summary>
        public AcademicEvent(int id, DateTime date, string name, string description, string type, string badgeColor)
        {
            EventId = id;
            EventDate = date.Date;
            EventName = name;
            Description = description;
            EventType = type;
            BadgeColor = badgeColor;
        }
    }
}
